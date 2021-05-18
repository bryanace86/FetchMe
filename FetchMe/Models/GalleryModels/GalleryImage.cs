using FetchMe.Models.PhotographModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.GalleryModels
{
    public class GalleryImage
    {
        public Guid GalleryId { get; set; }
        public virtual Gallery Gallery{get;set;}

        public Guid PhotographId { get; set; }
        public virtual Photograph Photograph { get; set; }
    }
}
