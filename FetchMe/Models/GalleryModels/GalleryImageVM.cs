using FetchMe.Models.PhotographModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.GalleryModels
{
    public class GalleryImageVM
    {
        public Guid GalleryId { get; set; }
        public Guid PhotographId { get; set; }
    }
}
