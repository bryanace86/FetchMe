using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FetchMe.Models.GalleryModels;
using FetchMe.Services.GalleryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FetchMe.API
{
    public class GalleriesAPIController : Controller
    {
        private readonly IGalleryService _galleryService;

        public GalleriesAPIController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost("api/[controller]/[action]")]
        public async Task<IActionResult> PostGallery(GalleryCreateVM galleryCreateVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UpsertImageInGalleryVM gallery = await _galleryService.Create(galleryCreateVM);

            if (gallery != null)
            {
                return Ok(gallery);
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost("api/[controller]/[action]")]
        public async Task<IActionResult> AddImageToGallery(GalleryImageVM galleryImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool success = await _galleryService.AddImageToGallery(galleryImage);

            if (success)
            {
                return Ok();
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost("api/[controller]/[action]")]
        public async Task<IActionResult> RemoveImageFromGallery(GalleryImageVM galleryImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool success = await _galleryService.RemoveImageFromGallery(galleryImage);

            if (success)
            {
                return Ok();
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
