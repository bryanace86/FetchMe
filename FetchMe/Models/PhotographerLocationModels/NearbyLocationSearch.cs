using FetchMe.Models.LocationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographerLocationModels
{
    public class NearbyLocationSearch
    {
        public Location Location { get; set; }
        public int Distance { get; set; }
        public bool PhotographerHas { get; set; }
        public string Direction { get; set; }
    }
}
