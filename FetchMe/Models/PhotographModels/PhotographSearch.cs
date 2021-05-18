using FetchMe.Models.LocationModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographModels
{
    public class PhotographSearch
    {
        public Location Location { get; set; }

        public int Distance { get; set; }


        [Display(Name = "Photographers Slug")]
        public string PhotographerSlug { get; set; }

        public Guid? GalleryId { get; set; }

        [Display(Name = "Tags")]
        public bool ShowFavorites { get; set; }


        [Display(Name = "Tags")]
        public List<string> PhotographTagIds { get; set; }


        [Display(Name = "Camera")]
        public List<string> CameraBody { get; set; }


        [Display(Name = "SS")]
        public List<string> ExposureTime { get; set; }


        [Display(Name = "f/")]
        public List<string> FStop { get; set; }


        [Display(Name = "ISO")]
        public List<string> ISO { get; set; }


        [Display(Name = "Lighting")]
        public List<string> LightSource { get; set; }


        [Display(Name = "Lens")]
        public List<string> Lens { get; set; }


        [Display(Name = "Focal Length")]
        public List<string> FocalLength { get; set; }

        public Guid? CurrentLastItem { get; set; }
        public DateTime? CurrentLastItemCreated { get; set; }

        public Guid? CurrentFirstItem { get; set; }
        public DateTime? CurrentFirstItemCreated { get; set; }


        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
