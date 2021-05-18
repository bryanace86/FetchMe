using AutoMapper;
using FetchMe.Data;
using FetchMe.Models.LocationModels;
using FetchMe.Models.PhotographerLocationModels;
using FetchMe.Models.PhotographerModels;
using FetchMe.Services.LocationServices;
using Geolocation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace FetchMe.Services.PhotographerLocationsServices
{
    public class PhotographerLocationService : IPhotographerLocationService
    {

        private readonly ApplicationDbContext _context;
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _userId;
        private readonly Guid? _photographerId;

        public PhotographerLocationService(
            ApplicationDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILocationService locationService)
        {
            _context = context;
            _locationService = locationService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _photographerId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue("PhotographerId"));
        }

        [Authorize]
        public async Task<IActionResult> DeletePhotographersCity(string locationId)
        {
            try
            {
                Photographer photographer = _context.Photographers.FirstOrDefault(x => x.OwnerID == _userId);

                Location location = _context.Locations.FirstOrDefault(x => x.Id == locationId);

                PhotographerLocation PhotographerLocation = _context.PhotographerLocations.FirstOrDefault(x => x.Location == location && x.Photographer == photographer);
                _context.PhotographerLocations.Remove(PhotographerLocation);
                await _context.SaveChangesAsync();

                return new StatusCodeResult(StatusCodes.Status200OK);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            }



        }

        [Authorize]
        public async Task<IActionResult> AddPhotographersCity(string locationId)
        {
            try
            {
                Photographer photographer = _context.Photographers.FirstOrDefault(x => x.OwnerID == _userId);

                Location location = _context.Locations.FirstOrDefault(x => x.Id == locationId);

                PhotographerLocation PhotographerLocation = new PhotographerLocation() { PhotographerId = photographer.Id, LocationId = location.Id };
                _context.PhotographerLocations.Add(PhotographerLocation);
                await _context.SaveChangesAsync();

                return new StatusCodeResult(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            }
        }

        [Authorize]
        public async Task<IActionResult> AddAllPhotographersCity(List<string> locationIds)
        {
            try
            {
                Photographer photographer = _context.Photographers.FirstOrDefault(x => x.OwnerID == _userId);

                foreach (string locationId in locationIds)
                {
                    _context.PhotographerLocations.Add(new PhotographerLocation() { PhotographerId = photographer.Id, LocationId = locationId });
                }

                await _context.SaveChangesAsync();

                return new StatusCodeResult(StatusCodes.Status200OK);
            }
            catch(Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            }
        }

        [Authorize]
        public async Task<IActionResult> DeleteAllPhotographersCity(List<string> locationIds)
        {
            try
            {
                Photographer photographer = _context.Photographers.FirstOrDefault(x => x.OwnerID == _userId);

                //string[] locIds = locationIds.Split(',');

                foreach (string locationId in locationIds)
                {
                    _context.PhotographerLocations.Remove(new PhotographerLocation() { PhotographerId = photographer.Id, LocationId = locationId });
                }

                await _context.SaveChangesAsync();

                return new StatusCodeResult(StatusCodes.Status200OK);
                //return new JsonResult("Ok");
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            }
        }

        [Authorize]
        public async Task<IEnumerable<PhotographersServiceableLocationCreateIndexItem>> GetLocations(NearbyLocationSearch search)
        {
            Photographer photographer = _context.Photographers.FirstOrDefault(x => x.Id == _photographerId);

            search.Location.Serviceable = true;

            //insert location if it does not exist
            Location selectedLocation = await _locationService.GetUpdatedLocation(search.Location);

            //get all locations then include the photographers where the photographer is logged in
            IQueryable<Location> locations = _locationService.GetLocationsByRadius(search.Location.Lat, search.Location.Lng, search.Distance, true)
                .IncludeFilter(x => x.Photographers.Where(x => x.PhotographerId == photographer.Id));

            //Map to viewModel
            IEnumerable<PhotographersServiceableLocationCreateIndexItem> locationsVM =
                _mapper.Map<IEnumerable<PhotographersServiceableLocationCreateIndexItem>>(locations);

            //add distance and direction
            foreach (PhotographersServiceableLocationCreateIndexItem loc in locationsVM)
            {
                loc.Distance = (int)GeoCalculator.GetDistance((double)search.Location.Lat, (double)search.Location.Lng, Convert.ToDouble(loc.Lat), Convert.ToDouble(loc.Lng), 0, 0);
                loc.Direction = GeoCalculator.GetDirection((double)search.Location.Lat, (double)search.Location.Lng, Convert.ToDouble(loc.Lat), Convert.ToDouble(loc.Lng));
            }

            //return results
            return locationsVM.OrderBy(x => x.Distance);

        }

        [Authorize]
        public async Task<List<Location>> GetPhotographersLocations()
        {
            return await _context.PhotographerLocations
                .Include(x => x.Location)
                .Where(x => x.PhotographerId == _photographerId)
                .Select(x => x.Location)
                .ToListAsync();
        }
    }
}
