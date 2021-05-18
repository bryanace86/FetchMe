using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.GalleryModels
{
    public class GalleryCreateVM
    {
        public string Title { get; set; }
        public bool IsProfessional { get; set; }
        public bool IsPublic { get; set; }
        public Guid? ImageId { get; set; }
    }
}
