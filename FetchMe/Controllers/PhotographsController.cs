using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FetchMe.Models.PhotographModels;
using FetchMe.Services.PhotographerServices;
using FetchMe.Services.PhotographServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vereyon.Web;

namespace FetchMe.Controllers
{
    public class PhotographsController : Controller
    {
        private readonly IPhotographService _photographService;
        private readonly IPhotographerService _photographerService;
        private readonly IFlashMessage _flashMessage;
        public PhotographsController(
            IPhotographService photographService,
            IPhotographerService photographerService,
            IFlashMessage flashMessage
            )
        {
            _photographService = photographService;
            _photographerService = photographerService;
            _flashMessage = flashMessage;
        }

        [Route("Photographs")]
        public async Task<IActionResult> Index(PhotographSearch search)
        {
            search.PageSize = 50;
            List<PhotographIndexViewModel> photographerDtos = await PhotographSearch(search);
            PhotographSearchResults photogrphResults = new PhotographSearchResults()
            {
                Search = search,
                Photographs = photographerDtos

            };
            if (_photographerService.UserHasPhotographer()) {
                ViewData["ImagesRemaining"] = 1000 - _photographService.GetPhotographCount().Result;
            }
            
            return View(photogrphResults);
        }

        [HttpPost("Photographs/PhotographSearch")]
        [IgnoreAntiforgeryToken]
        public async Task<List<PhotographIndexViewModel>> PhotographSearch(PhotographSearch search)
        {
            return await _photographService.GetAllAsync(search);
        }

        [Route("Photograph/{id}")]
        public async Task<IActionResult> DetailsAsync(Guid id)
        {
            
            PhotographDto photographerDto = await _photographService.GetById(id);
            return View(photographerDto);
        }

        [Authorize]
        [HttpGet]
        [Route("Photographs/Create")]
        public async Task<IActionResult> CreateAsync()
        {
            int photographerImageCount = await _photographService.GetPhotographCount();
            if (photographerImageCount > 1000) return RedirectToAction("Index");
            ViewData["ImagesRemaining"] = 1000 - photographerImageCount;

            return View();
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost("Photographs/Create")]
        public async Task<IActionResult> CreateAsync(PhotographCreateDto photograph)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (await _photographService.GetPhotographCount() > 1000) return BadRequest(ModelState);

            var photographDto = await _photographService.CreateAsync(photograph);

            return Ok();
        }

        [Authorize]
        [HttpGet("Photographs/Update/{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id)
        {
            var photographDto = await _photographService.GetByIdAsync(id);
            PhotographUpdateDto photographUpdateDto = await _photographService.MapToUpdateDto(photographDto);
            
            if(photographUpdateDto == null) return RedirectToAction("Index");

            return View(photographUpdateDto);
        }

        [Authorize]
        [HttpPost("Photographs/UpdateAsync")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(PhotographUpdateDto photograph)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var photographDto = await _photographService.UpdateAsync(photograph);
            if (photographDto == null) {
                _flashMessage.Danger("There was a problem updating your photograph.");
                return View(photograph);
            }
            _flashMessage.Info("Photograph updated successfully.");
            return RedirectToAction("Index");
        }


        [Authorize]
        [HttpGet("Photographs/Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var photographDto = await _photographService.GetByIdAsync(id);

            if (photographDto == null) return RedirectToAction("Index");

            if (! await _photographService.IsOwnedByPhotographer(photographDto)) return RedirectToAction("Index");

            return View(photographDto);
        }

        [Authorize]
        [HttpPost("Photographs/Delete/{id}")]
        public async Task<IActionResult> DeletedAsync(Guid id)
        {
            if (await _photographService.DeleteAsync(id)) {
                _flashMessage.Info("Image deleted successfully.");
            }
            else
            {
                _flashMessage.Danger("There was a problem deleting the image");
            }
            
            return RedirectToAction("index");

        }
    }
}
