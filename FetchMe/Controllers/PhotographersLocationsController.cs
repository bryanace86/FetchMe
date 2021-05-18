using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FetchMe.Models.LocationModels;
using FetchMe.Models.PhotographerLocationModels;
using FetchMe.Services.PhotographerLocationsServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FetchMe.Controllers
{
    public class PhotographersLocationsController : Controller
    {
        private readonly IPhotographerLocationService _photographerLocationService;
        public PhotographersLocationsController(
            IPhotographerLocationService photographerLocationService
            )
        {
            _photographerLocationService = photographerLocationService;
        }

        // GET: PhotographerServiceableCities/Create
        [Route("[controller]/[action]")]
        public async Task<IActionResult> CreateAsync()
        {
            List<Location> photographersLocations = await _photographerLocationService.GetPhotographersLocations();
            PhotographerLocationIndex photographerCityIndex = new PhotographerLocationIndex()
            {
                Search = new NearbyLocationSearch(),
                PhotographerLocations = photographersLocations
            };

            return View(photographerCityIndex);
        }

        [Authorize]
        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> DeletePhotographersCity([FromBody] string locationId)
        {
            var result = await _photographerLocationService.DeletePhotographersCity(locationId);
            return result;
        }

        [Authorize]
        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> AddPhotographersCity([FromBody] string locationId)
        {
            var result = await _photographerLocationService.AddPhotographersCity(locationId);
            return result;
        }

        [Authorize]
        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> AddAllPhotographersCity([FromBody] List<string> locationIds)
        {
            var result = await _photographerLocationService.AddAllPhotographersCity(locationIds);
            return result;
        }

        [Authorize]
        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> DeleteAllPhotographersCity([FromBody] List<string> locationIds)
        {
            var result = await _photographerLocationService.DeleteAllPhotographersCity(locationIds);
            return result;
        }

        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> GetLocations(NearbyLocationSearch search)
        {
            var result = await _photographerLocationService.GetLocations(search);
            return Ok(result);
        }
    }
}
