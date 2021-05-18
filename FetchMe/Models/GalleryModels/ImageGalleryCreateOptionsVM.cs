using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.GalleryModels
{
    public class ImageGalleryCreateOptionsVM
    {
        public Guid ImageId { get; set; }
        public bool IsOwnerOfImage { get; set; }
    }
}
