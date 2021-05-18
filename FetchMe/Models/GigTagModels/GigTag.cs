using FetchMe.Models.GigModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.GigTagModels
{
    public class GigTag
    {
        public string Id { get; set; }
        public virtual List<GigTagGig> Gigs { get; set; }
    }
}
