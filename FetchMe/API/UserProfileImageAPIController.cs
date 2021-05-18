using FetchMe.Models.PhotographModels;
using FetchMe.Services.UserProfileImageService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.API
{
    public class UserProfileImageAPIController : Controller
    {
        private readonly IUserProfileImageService _userProfileImageService;

        public UserProfileImageAPIController(
            IUserProfileImageService userProfileImageService)
        {
            _userProfileImageService = userProfileImageService;
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost("api/[controller]/[action]")]
        public async Task<IActionResult> PostUserProfileImage(UserProfileImageUpsert input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserProfileImageDto userProfileImageDto = await _userProfileImageService.CreateAsync(input);

            if (userProfileImageDto != null)
            {
                return Ok(userProfileImageDto);
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
