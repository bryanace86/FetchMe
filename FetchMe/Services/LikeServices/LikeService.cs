using AutoMapper;
using FetchMe.Data;
using FetchMe.Models;
using FetchMe.Models.LikeModels;
using FetchMe.Services.PhotographerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Services.LikeServices
{
    public class LikeService : ILikeService
    {

        private readonly ILogger<PhotographerService> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<IdentityUser> _roleManager;

        public LikeService(
            ILogger<PhotographerService> logger,
            ApplicationDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager
            //RoleManager<IdentityUser> roleManager
            )
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            //_roleManager = roleManager;
        }
        public async Task<bool> CreateLike(AlterLikeDto like)
        {
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                string userId = user.Id;

                //switch to determine the appropriate table
                switch (like.Type)
                {
                    case "Photographer":
                        if (!_context.PhotographerLikes.Any(x => x.UserId == userId && x.PhotographerId == like.Id))
                        {
                            PhotographerLike photographerLike = new PhotographerLike() { UserId = userId, PhotographerId = like.Id };
                            _context.PhotographerLikes.Add(photographerLike);
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        break;
                    case "Photograph":
                        if (!_context.PhotographLikes.Any(x => x.UserId == userId && x.PhotographId == like.Id))
                        {
                            PhotographLike photographLike = new PhotographLike() { UserId = userId, PhotographId = like.Id };
                            _context.PhotographLikes.Add(photographLike);
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        break;
                    default:
                        return false;
                }
                return false;
            }
            catch
            {
                return false;

            }
        }

        public async Task<bool> DeleteLike(AlterLikeDto like)
        {
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                string userId = user.Id;

                switch (like.Type)
                {
                    case "Photographer":
                        PhotographerLike photographerLike = _context.PhotographerLikes.FirstOrDefault(x => x.UserId == userId && x.PhotographerId == like.Id);
                        _context.PhotographerLikes.Remove(photographerLike);
                        await _context.SaveChangesAsync();
                        return true;
                    case "Photograph":
                        PhotographLike photographLike = _context.PhotographLikes.FirstOrDefault(x => x.UserId == userId && x.PhotographId == like.Id);
                        _context.PhotographLikes.Remove(photographLike);
                        await _context.SaveChangesAsync();
                        return true;
                    default: 
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
