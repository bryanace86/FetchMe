using FetchMe.Models.CameraBodyModels;
using FetchMe.Models.ExposureTimeModels;
using FetchMe.Models.FocalLengthModels;
using FetchMe.Models.FStopModels;
using FetchMe.Models.ISOModels;
using FetchMe.Models.LensModels;
using FetchMe.Models.LightSourceModels;
using FetchMe.Models.LocationModels;
using FetchMe.Models.PhotographTagModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographModels
{
    public class PhotographUpdateDto
    {
        public Guid Id { get; set; }

        public Guid PhotographerId { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_ \-]*$", ErrorMessage = "Special characters should not be entered")]
        [Display(Name = "Image Title")]
        public string ImageTitle { get; set; }

        [Display(Name = "URL")]
        public string ImageUrl { get; set; }

        [Display(Name = "Description")]
        public string ImageDescription { get; set; }

        [Display(Name = "Tags")]
        public List<string> PhotographTagIds { get; set; }
        public List<PhotographTagsPhotograph> PhotographTags { get; set; }

        [Display(Name = "Camera")]
        public string CameraBodyValue { get; set; }
        public CameraBody CameraBody { get; set; }

        [Display(Name = "SS")]
        public string ExposureTimeValue { get; set; }
        public ExposureTime ExposureTime { get; set; }

        [Display(Name = "f/")]
        public string FStopValue { get; set; }
        public FStop FStop { get; set; }

        [Display(Name = "ISO")]
        public string ISOValue { get; set; }
        public ISO ISO { get; set; }

        [Display(Name = "Lighting")]
        public string LightSourceValue { get; set; }
        public LightSource LightSource { get; set; }

        [Display(Name = "Lens")]
        public string LensValue { get; set; }
        public Lens Lens { get; set; }

        [Display(Name = "Focal Length")]
        public string FocalLengthValue { get; set; }
        public FocalLength FocalLength { get; set; } = new FocalLength();

        [Display(Name = "Location")]
        public string LocationId { get; set; }
        public Location Location { get; set; }

        [Display(Name = "Photographer Banner Image")]
        public bool IsBannerImage { get; set; }

        [Display(Name = "Photographer Logo Image")]
        public bool IsLogoImage { get; set; }

        [Display(Name = "Hide From Gallery")]
        public bool HideFromGallery { get; set; }
    }
}
