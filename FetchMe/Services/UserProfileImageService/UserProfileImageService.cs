using AutoMapper;
using FetchMe.Data;
using FetchMe.Models;
using FetchMe.Models.PhotographModels;
using FetchMe.Services.BlobServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FetchMe.Services.UserProfileImageService
{
    public class UserProfileImageService : IUserProfileImageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBlobService _blobService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;


        public UserProfileImageService(
            ApplicationDbContext context,
            IBlobService blobService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _blobService = blobService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<UserProfileImageDto> CreateAsync(UserProfileImageUpsert input)
        {
            Guid imageId = Guid.NewGuid();
            //Upload Image
            byte[] data = Convert.FromBase64String(input.ImageData);
            PhotographDimension photographDimensions = await _blobService.ResizeBase64Async(imageId.ToString(), data, 300, 90);

            if (photographDimensions == null) throw new Exception();

            //Save image in database
            ApplicationUser user = await _context.Users
                .Include(x => x.UserProfileImage)
                .FirstOrDefaultAsync(x => x.Id == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            UserProfileImage userProfileImage = new UserProfileImage()
            {
                Id = imageId,
                UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                ImageTitle = imageId.ToString(),
                ImageUrl = imageId.ToString(),
                Created = DateTime.Now,
                Width = photographDimensions.Width,
                Height = photographDimensions.Height
            };

            _context.UserProfileImages.Add(userProfileImage);

            //return userBannerImage;
            user.UserProfileImageId = imageId;
            user.UserProfileImage = userProfileImage;

            _context.Users.Update(user);
            _context.SaveChanges();

            UserProfileImageDto userBannerImageDto = _mapper.Map<UserProfileImageDto>(userProfileImage);

            return userBannerImageDto;
            
        }

    }
}
