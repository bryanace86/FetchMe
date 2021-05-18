using FetchMe.Models.CameraBodyModels;
using FetchMe.Models.ExposureTimeModels;
using FetchMe.Models.FocalLengthModels;
using FetchMe.Models.FStopModels;
using FetchMe.Models.GalleryModels;
using FetchMe.Models.ISOModels;
using FetchMe.Models.LensModels;
using FetchMe.Models.LightSourceModels;
using FetchMe.Models.LikeModels;
using FetchMe.Models.LocationModels;
using FetchMe.Models.PhotographTagModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographModels
{
    public class PhotographBase
    {
        public Guid Id { get; set; }

        public string ImageTitle { get; set; }

        public string ImageDescription { get; set; }

        public string ImageUrl { get; set; }

        public bool HideFromGallery { get; set; }

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
