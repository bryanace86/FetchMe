using FetchMe.Models.LocationModels;
using FetchMe.Models.PhotographerLocationModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Services.PhotographerLocationsServices
{
    public interface IPhotographerLocationService
    {
        Task<IActionResult> DeletePhotographersCity(string locationId);
        Task<IActionResult> AddPhotographersCity(string locationId);
        Task<IActionResult> AddAllPhotographersCity( List<string> locationIds);
        Task<IActionResult> DeleteAllPhotographersCity( List<string> locationIds);
        Task<IEnumerable<PhotographersServiceableLocationCreateIndexItem>> GetLocations(NearbyLocationSearch search);
        Task<List<Location>> GetPhotographersLocations();
    }
}
