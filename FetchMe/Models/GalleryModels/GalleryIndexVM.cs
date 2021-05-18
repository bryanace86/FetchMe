using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.GalleryModels
{
    public class GalleryIndexVM
    {
        public Guid? ImageId { get; set; }
        public GallerySearch Search {get;set;}
        public IEnumerable<GalleryIndexItemVM> Galleries { get; set; }
    }
}
