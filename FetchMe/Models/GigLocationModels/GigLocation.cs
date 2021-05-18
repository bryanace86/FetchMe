using FetchMe.Models.GigModels;
using FetchMe.Models.LocationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.GigLocationModels
{
    public class GigLocation
    {
        public Guid GigId { get; set; }
        public virtual Gig Gig { get; set; }

        public string LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
