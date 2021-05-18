using FetchMe.Models.GalleryModels;
using FetchMe.Models.UserModels;
using FetchMe.Services.ApplicationUserServices;
using FetchMe.Services.GalleryServices;
using FetchMe.Services.UserProfileImageService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vereyon.Web;

namespace FetchMe.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IFlashMessage _flashMessage;
        private readonly IGalleryService _galleryService;
        private readonly IApplicationUserService _applicationUserService;

        public UserProfileController(
            IFlashMessage flashMessage,
            IGalleryService galleryService,
            IApplicationUserService applicationUserService
            )
        {
            _flashMessage = flashMessage;
            _galleryService = galleryService;
            _applicationUserService = applicationUserService;
        }

        [HttpGet("User/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            ApplicationUserDetailsVm applicationUserDetailsVm = await _applicationUserService.GetBySlugAsync(slug);

            applicationUserDetailsVm.Galleries = await _galleryService.GetGalleries(new GallerySearch()
            {
                OwnerId = applicationUserDetailsVm.Id
            });

            return View(applicationUserDetailsVm);
        }

        [HttpGet("[Controller]/[Action]")]
        [Authorize]
        public async Task<IActionResult> UpdateDetails()
        {
            ApplicationUserDetailsVm applicationUserDetailsVm = await _applicationUserService.GetUsersDetailsVMAsync();
            if (applicationUserDetailsVm == null)
            {
                return Redirect("/Home/Index");
            }
            return View(applicationUserDetailsVm);
        }

        [HttpPost("[Controller]/[Action]")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDetails(ApplicationUserDetailsVm userVm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ApplicationUserDetailsVm applicationUserDetailsVm = await _applicationUserService.UpdateUserDetailsVMAsync(userVm);
            if(applicationUserDetailsVm == null)
            {
                return RedirectToAction(nameof(UpdateDetails), userVm);
            }

            return RedirectToAction("Details", new { slug = applicationUserDetailsVm.Slug });
        }

    }
}
