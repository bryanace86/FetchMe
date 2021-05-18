using FetchMe.Data;
using FetchMe.Models.LocationModels;
using Geolocation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Services.LocationServices
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;
        public LocationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Delete(Location location)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Location>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Location> GetById(string id)
        {
            return await _context.Locations.FirstOrDefaultAsync(x =>
                x.Id == id
            );
        }

        public async Task<Location> GetByLocation(Location location)
        {
            return await _context.Locations.FirstOrDefaultAsync(x =>
                x.FormattedAddress == location.FormattedAddress &&
                x.AdministrativeAreaLevel1 == location.AdministrativeAreaLevel1 &&
                x.AdministrativeAreaLevel2 == location.AdministrativeAreaLevel2 &&
                x.AdministrativeAreaLevel3 == location.AdministrativeAreaLevel3 &&
                x.Country == location.Country &&
                x.Locality == location.Locality &&
                x.Neighborhood == location.Neighborhood &&
                x.Political == location.Political &&
                x.PostalCode == location.PostalCode &&
                x.Route == location.Route &&
                x.StreetNumber == location.StreetNumber
            );

        }

        public IQueryable<Location> GetLocationsByRadius(decimal? lat, decimal? lng, int radius, bool serviceable)
        {
            var CenterLat = Convert.ToDouble(lat);
            var Centerlong = Convert.ToDouble(lng);
            var distance = radius;
            CoordinateBoundaries boundaries = new CoordinateBoundaries(CenterLat, Centerlong, distance);

            Decimal minLatitude = Convert.ToDecimal(boundaries.MinLatitude);
            Decimal maxLatitude = Convert.ToDecimal(boundaries.MaxLatitude);
            Decimal minLongitude = Convert.ToDecimal(boundaries.MinLongitude);
            Decimal maxLongitude = Convert.ToDecimal(boundaries.MaxLongitude);

            IQueryable<Location> locations = _context.Locations;

            if (serviceable) locations = locations.Where(x => x.Serviceable);

            return locations
                .Where(x =>
                    x.Lat >= minLatitude && x.Lat <= maxLatitude &&
                    x.Lng >= minLongitude && x.Lng <= maxLongitude
                    );

        }

        public async Task<Location> GetUpdatedLocation(Location location)
        {
            Location check = await GetById(location.Id);

            if (check == null)
            {
                //insert location
                //search.Location.Serviceable = true;
                _context.Locations.Add(location);
                _context.SaveChanges();
                return location;

            }
            else if (location.Serviceable && !check.Serviceable)
            {
                //update locations
                check.Serviceable = true;
                _context.Locations.Update(check);
                _context.SaveChanges();
                return check;
            }

            return check;

        }

        public async Task Insert(Location location)
        {
            await _context.Locations.AddAsync(location);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Location location)
        {
            throw new NotImplementedException();
        }
    }
}
