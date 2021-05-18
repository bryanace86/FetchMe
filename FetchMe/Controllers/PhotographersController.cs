using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FetchMe.Models.GalleryModels;
using FetchMe.Models.PhotographerModels;
using FetchMe.Models.PhotographModels;
using FetchMe.Services.GalleryServices;
using FetchMe.Services.PhotographerServices;
using FetchMe.Services.PhotographServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vereyon.Web;

namespace FetchMe.Controllers
{
    public class PhotographersController : Controller
    {

        private readonly IPhotographerService _photographerService;
        private readonly IPhotographService _photographService;
        private readonly IFlashMessage _flashMessage;
        private readonly IGalleryService _galleryService;

        public PhotographersController(
            IPhotographerService photographerService,
            IFlashMessage flashMessage,
            IPhotographService photographService,
            IGalleryService galleryService
            )
        {
            _photographerService = photographerService;
            _flashMessage = flashMessage;
            _photographService = photographService;
            _galleryService = galleryService;
        }

        //[Route("Photographers")]
        [HttpGet("[controller]")]
        public async Task<IActionResult> Index(PhotographerSearch search)
        {
            if (search.PageSize == 0) search.PageSize = 10;

            List<PhotographerIndexViewModel> photographerDtos = await _photographerService.GetAllAsync(search);
            /*
            foreach(PhotographerDto photographerDto in photographerDtos)
            {
                photographerDto.Photographs = await _photographService.GetAllAsync(new PhotographSearch()
                {
                    PhotographerSlug = photographerDto.Slug,
                    PhotographTagIds = search.PhotographTagIds,
                });
            }
            */
            PhotographerSearchResults photogrpherResults = new PhotographerSearchResults()
            {
                Search = search,
                Photographers = photographerDtos

            };

            return View(photogrpherResults);
        }

        [HttpGet("Photographers/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            //PhotographerDto photographerDto = _photographerService.GetBySlug(slug);
            PhotographerDetailsDto photographerDetails = _photographerService.GetPhotographerDetails(slug);
            photographerDetails.Photographs = await _photographService.GetAllAsync(new PhotographSearch()
            {
                PhotographerSlug = slug
            });
            photographerDetails.GalleryIndex = await _galleryService.GetGalleries(new GallerySearch()
            {
                PhotographerId = photographerDetails.Id
            });
            
            return View(photographerDetails);
        }

        [Authorize]
        [HttpGet]
        [Route("Photographer/Create")]
        public IActionResult Create()
        {
            if (_photographerService.UserHasPhotographer()) return RedirectToAction("Index");
            return View();
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost("Photographer/Create")]
        public async Task<IActionResult> Create(PhotographerCreateDto photographer)
        {
            if (!ModelState.IsValid) return View(photographer);
            if (_photographerService.UserHasPhotographer()) return RedirectToAction("Index");
            
            var photographerDto = await _photographerService.CreateAsync(photographer);

            _flashMessage.Info("Welcome to your profile. You should add some photographs next...");

            return RedirectToAction("Details", new { slug = photographer.Slug });
        }


        [Authorize]
        [HttpGet("Photographer/Update/{slug}")]
        public async Task<IActionResult> UpdateAsync(string slug)
        {
            if (!_photographerService.UserHasPhotographer()) return RedirectToAction("Index");
            PhotographerDto photographerDto = _photographerService.GetBySlug(slug);
            if (photographerDto == null) {
                _flashMessage.Danger("You are not authorized to update this profile.");
                return RedirectToAction("index");
            }
            PhotographerUpdateDto photographerUpdateDto = await _photographerService.MapToUpdateDtoAsync(photographerDto);
            return View(photographerUpdateDto);
        }

        [Authorize]
        [HttpPost("Photographers/UpdateAsync")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(PhotographerUpdateDto photographerDto)
        {
            PhotographerDto result = await _photographerService.UpdateAsync(photographerDto);
            if (result != null)
            {
                _flashMessage.Info("Profile updated successfully.");
            }
            else
            {
                _flashMessage.Danger("There was a problem updating your profile");
            }

            return RedirectToAction("Details", new { slug = result.Slug });

        }

        [Route("Photographer/IsSlugUnique")]
        public async Task<IActionResult> IsSlugUnique(string slug)
        {
            if (_photographerService.UserHasPhotographer())
            {
                PhotographerDto userPhotographer = await _photographerService.GetByUser();
                if (userPhotographer.Slug == slug) 
                    return Json(true);
            }

            if(!_photographerService.CheckSlugExist(slug)) 
                return Json(true);

            return Json(false);
        }
    }
}
