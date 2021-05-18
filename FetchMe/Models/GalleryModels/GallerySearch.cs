using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.GalleryModels
{
    public class GallerySearch
    {
        [Display(Name = "Photographers Slug")]
        public string OwnerId { get; set; }
        public Guid? PhotographerId { get; set; }
        public bool IsProfessional { get; set; }
    }
}
