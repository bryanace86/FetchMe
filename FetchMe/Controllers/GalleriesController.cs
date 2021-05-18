using FetchMe.API;
using FetchMe.Models.GalleryModels;
using FetchMe.Services.GalleryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Controllers
{
    public class GalleriesController : Controller
    {
        private readonly IGalleryService _galleryService;

        public GalleriesController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> PostGallery(GalleryCreateVM galleryCreateVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UpsertImageInGalleryVM gallery = await _galleryService.Create(galleryCreateVM);

            if (gallery != null)
            {
                return PartialView("~/Views/Galleries/AddToGalleryIndexItem.cshtml", gallery);
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpGet("[controller]/[action]/{imageId}")]
        public async Task<IActionResult> GetAddToGalleryForm(Guid imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UpsertImageInGalleryGrouped galleries = await _galleryService.GetUsersGalleriesForImageAsync(imageId);

            if (galleries != null)
            {
                return PartialView("~/Views/Galleries/AddImageToGalleryList.cshtml", galleries);
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        
        [Authorize]
        [HttpGet("[controller]/[action]/{imageId}")]
        public async Task<IActionResult> GetCreateGalleryForm(Guid imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ImageGalleryCreateOptionsVM galleryOptions = await _galleryService.GetGalleryOptionsForImageAsync(imageId);

            if (galleryOptions != null)
            {
                return PartialView("~/Views/Galleries/CreateGalleryForm.cshtml", galleryOptions);
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
