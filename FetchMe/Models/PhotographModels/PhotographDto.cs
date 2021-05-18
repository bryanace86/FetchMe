using FetchMe.Models.CameraBodyModels;
using FetchMe.Models.ExposureTimeModels;
using FetchMe.Models.FocalLengthModels;
using FetchMe.Models.FStopModels;
using FetchMe.Models.ISOModels;
using FetchMe.Models.LensModels;
using FetchMe.Models.LightSourceModels;
using FetchMe.Models.LikeModels;
using FetchMe.Models.LocationModels;
using FetchMe.Models.PhotographerModels;
using FetchMe.Models.PhotographTagModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographModels
{
    public class PhotographDto
    {
        public Guid Id { get; set; }

        public string ImageTitle { get; set; }

        public string ImageDescription { get; set; }

        public string ImageUrl { get; set; }

        public bool HideFromGallery { get; set; }

        // 1 to 1


        // 1 to m
        public Guid PhotographerId { get; set; }
        public virtual Photographer Photographer { get; set; }

        public string CameraBodyValue { get; set; }
        public virtual CameraBody CameraBody { get; set; }

        public string ExposureTimeValue { get; set; }
        public virtual ExposureTime ExposureTime { get; set; }

        public string FStopValue { get; set; }
        public virtual FStop FStop { get; set; }

        public string ISOValue { get; set; }
        public virtual ISO ISO { get; set; }

        public string LightSourceValue { get; set; }
        public virtual LightSource LightSource { get; set; }

        public string LensValue { get; set; }
        public virtual Lens Lens { get; set; }

        public string FocalLengthValue { get; set; }
        public virtual FocalLength FocalLength { get; set; }

        public string LocationId { get; set; }
        public virtual Location Location { get; set; }

        // m to m
        public virtual List<PhotographTagsPhotograph> PhotographTags { get; set; }
        public virtual List<PhotographLike> PhotographLikes { get; set; }
        public bool IsLikedByUser { get; internal set; }
        public bool IsOwner { get; internal set; }

        //Auditing
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
