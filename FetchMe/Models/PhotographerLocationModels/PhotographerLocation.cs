using FetchMe.Models.LocationModels;
using FetchMe.Models.PhotographerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographerLocationModels
{
    public class PhotographerLocation
    {
        public Guid PhotographerId { get; set; }
        public Photographer Photographer { get; set; }

        public string LocationId { get; set; }
        public Location Location { get; set; }
    }
}
