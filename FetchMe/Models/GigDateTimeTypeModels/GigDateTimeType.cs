using FetchMe.Models.GigModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.GigDateTimeTypeModels
{
    public class GigDateTimeType
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public virtual List<Gig> Gigs { get; set; }
    }
}
