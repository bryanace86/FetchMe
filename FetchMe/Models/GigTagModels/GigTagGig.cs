using FetchMe.Models.GigModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.GigTagModels
{
    public class GigTagGig
    {
        public Guid GigId { get; set; }
        public virtual Gig Gig { get; set; }

        public string TagId { get; set; }
        public virtual GigTag Tag { get; set; }
    }
}
