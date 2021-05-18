using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographerBadgeModels
{
    public class Badge
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Caption { get; set; }
        public string BadgeClass { get; set; }
        public string Graphic { get; set; }

        public virtual List<PhotographerBadge> PhotographerBadges { get; set; }
    }
}
