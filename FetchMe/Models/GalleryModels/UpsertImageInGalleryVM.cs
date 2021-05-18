using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.GalleryModels
{
    public class UpsertImageInGalleryVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public bool IsProfessional { get; set; }
        public bool IsPublic { get; set; }
        public bool IsInGallery { get; set; }
        public bool IsOwnerOfImage { get; set; }
    }
}
