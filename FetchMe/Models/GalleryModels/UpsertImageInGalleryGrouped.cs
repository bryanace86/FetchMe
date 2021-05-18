using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.GalleryModels
{
    public class UpsertImageInGalleryGrouped
    {
        public IEnumerable<UpsertImageInGalleryVM> InGallery { get; set; }
        public IEnumerable<UpsertImageInGalleryVM> NotInGallery { get; set; }
    }
}
