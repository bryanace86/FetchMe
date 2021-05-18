using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.GalleryModels
{
    public class GalleryIndexItemVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsProfessional { get; set; }
        public bool IsPublic { get; set; }
        public bool HasImage { get; set; }
        public Guid? ImageId { get; set; }
        public string Thumbnail { get; set; }
    }
}
