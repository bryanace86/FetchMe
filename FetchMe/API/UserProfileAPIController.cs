using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FetchMe.Models.UserModels;
using FetchMe.Services.ApplicationUserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FetchMe.API
{
    public class UserProfileAPIController : Controller
    {
        private readonly IApplicationUserService _applicationUserService;

        public UserProfileAPIController(
            IApplicationUserService applicationUserService
            )
        {
            _applicationUserService = applicationUserService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("UserProfileAPI/GetAllUserCompletenessAsync")]
        public async Task<IActionResult> GetAllUserCompletenessAsync()
        {
            List<ApplicationUserCompleteness> users = await _applicationUserService.GetAllUserCompletenessAsync();
            return Ok(users);
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> IsSlugUnique(string slug)
        {
            var user = await _applicationUserService.GetUsersDetailsVMAsync();
            if (user?.Slug != null)
            {
                if (user?.Slug == slug)
                    return Json(true);
            }

            if (!await _applicationUserService.CheckSlugExistAsync(slug))
                return Json(true);

            return Json(false);
        }
    }
}
