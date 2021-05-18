using FetchMe.Models.LocationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Services.LocationServices
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAll();
        //IQueryable<Location> GetLocationsByRadius(decimal lat, decimal lng, int radius, bool serviceable);

        Task<Location> GetById(string Id);
        Task<Location> GetByLocation(Location location);
        IQueryable<Location> GetLocationsByRadius(decimal? lat, decimal? lng, int radius, bool serviceable);
        Task<Location> GetUpdatedLocation(Location location);

        Task Insert(Location location);
        void Update(Location location);
        void Delete(Location location);
        Task Save();
    }
}
