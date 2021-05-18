using FetchMe.Models.PhotographerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.LikeModels
{
    public class PhotographerLike
    {
        public Guid Id { get; set; }

        public String UserId { get; set; }

        public virtual Photographer Photographer { get; set; }
        public Guid PhotographerId { get; set; }
    }
}
