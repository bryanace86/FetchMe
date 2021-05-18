using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographModels
{
    public class PhotographIndexViewModel
    {
        public Guid Id { get; set; }

        public string ImageTitle { get; set; }

        public string ImageDescription { get; set; }

        public string ImageUrl { get; set; }

        public bool HideFromGallery { get; set; }

        // 1 to 1


        // 1 to m
        public Guid PhotographerId { get; set; }
        public string PhotographersName { get; set; }
        public string PhotographersSlug { get; set; }


        public string CameraBodyValue { get; set; }

        public string ExposureTimeValue { get; set; }

        public string FStopValue { get; set; }

        public string ISOValue { get; set; }

        public string LightSourceValue { get; set; }

        public string LensValue { get; set; }

        public string FocalLengthValue { get; set; }

        public string LocationId { get; set; }
        public string FormattedAddress { get; set; }


        // m to m
        public List<string> PhotographTagIds { get; set; }
        public bool IsLikedByUser { get; set; }
        public bool IsOwner { get; set; }

        public DateTime Created { get; set; }

        //Dimensions
        public int Width { get; set; }
        public int Height { get; set; }

        public int SWidth { get; set; }
        public int SHeight { get; set; }

        public int MWidth { get; set; }
        public int MHeight { get; set; }

        public int LWidth { get; set; }
        public int LHeight { get; set; }
    }
}
