using FetchMe.Models.PhotographerModels;
using FetchMe.Models.PhotographModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.GalleryModels
{
    public class Gallery
    {
        public Guid Id { get; set; }
        public string OwnerID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string Title { get; set; }
        public Guid? PhotographerId { get; set; }
        public virtual Photographer Photographer { get; set; }
        public bool IsPublic { get; set; }
        public virtual IEnumerable<GalleryImage> GalleryImages { get; set; }
    }
}
