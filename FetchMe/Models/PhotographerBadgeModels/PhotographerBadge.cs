using FetchMe.Models.PhotographerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographerBadgeModels
{
    public class PhotographerBadge
    {
        public Guid BadgeId { get; set; }
        public Badge Badge { get; set; }

        public Guid PhotographerId { get; set; }
        public Photographer Photographer { get; set; }
    }
}
