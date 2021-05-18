using FetchMe.Models.LocationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographerLocationModels
{
    public class PhotographerLocationIndex
    {
        public NearbyLocationSearch Search { get; set; }
        public List<Location> PhotographerLocations { get; set; }
    }
}
