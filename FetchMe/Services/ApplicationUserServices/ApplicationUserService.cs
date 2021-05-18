using AutoMapper;
using FetchMe.Data;
using FetchMe.Models;
using FetchMe.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace FetchMe.Services.ApplicationUserServices
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public ApplicationUserService(
            ApplicationDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<ApplicationUserDetailsVm> GetUserDetailsVMAsync(string id)
        {
            ApplicationUser applicationUser = await _userManager.Users
                .Include(x => x.UserProfileImage)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (applicationUser == null) return null;

            ApplicationUserDetailsVm applicationUserDetailsVm = _mapper.Map<ApplicationUserDetailsVm>(applicationUser);

            applicationUserDetailsVm.IsOwner = applicationUserDetailsVm.Id == id;

            return applicationUserDetailsVm;
        }

        public async Task<ApplicationUserDetailsVm> GetUsersDetailsVMAsync()
        {
            ApplicationUserDetailsVm applicationUserDetailsVm = await GetUserDetailsVMAsync(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            return applicationUserDetailsVm;
        }

        public async Task<ApplicationUserDetailsVm> UpdateUserDetailsVMAsync(ApplicationUserDetailsVm userVm)
        {
            ApplicationUser applicationUser = await _userManager.Users
                .FirstOrDefaultAsync(x => x.Id == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            if (applicationUser == null) return null;

            applicationUser.Slug = userVm.Slug;
            applicationUser.FirstName = userVm.FirstName;
            applicationUser.LastName = userVm.LastName;
            applicationUser.DisplayName = userVm.DisplayName;

            await _userManager.UpdateAsync(applicationUser);

            if (_httpContextAccessor.HttpContext.User.HasClaim(x => x.Type == "UserSlug"))
            {
                await _userManager.RemoveClaimAsync(applicationUser, _userManager.GetClaimsAsync(applicationUser).Result.FirstOrDefault(x => x.Type == "UserSlug"));
            }
            
            await _userManager.AddClaimAsync(applicationUser, new Claim("UserSlug", applicationUser.Slug.ToString(), "string", "FetchMe"));
            await _signInManager.RefreshSignInAsync(applicationUser);

            ApplicationUserDetailsVm applicationUserDetailsVm = _mapper.Map<ApplicationUserDetailsVm>(applicationUser);

            return applicationUserDetailsVm;

        }

        public async Task<bool> CheckSlugExistAsync(string slug)
        {
            ApplicationUserDetailsVm applicationUser = await GetBySlugAsync(slug);
            return applicationUser != null ? true : false;
        }
        public async Task<ApplicationUserDetailsVm> GetBySlugAsync(string slug)
        {

            ApplicationUser applicationUser = await _userManager.Users
                .Include(x => x.UserProfileImage)
                .FirstOrDefaultAsync(x => x.Slug == slug);

            ApplicationUserDetailsVm applicationUserDto = _mapper.Map<ApplicationUserDetailsVm>(applicationUser);

            if (applicationUserDto == null) return null;

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                applicationUserDto.IsOwner = applicationUserDto.Id == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return applicationUserDto;
        }


        [Authorize(Roles = "Admin")]
        public async Task<List<ApplicationUserCompleteness>> GetAllUserCompletenessAsync()
        {
            List<ApplicationUser> users = await _context.Users
                .Include(x => x.Photographer)
                .ThenInclude(x => x.Photographs)
                .ToListAsync();

            List<ApplicationUserCompleteness> applicationUserCompleteness =
                _mapper.Map<List<ApplicationUserCompleteness>>(users);

            return applicationUserCompleteness;

        }
    }
}
