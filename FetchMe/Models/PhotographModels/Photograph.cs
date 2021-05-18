
using FetchMe.Models.BlogModels;
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
using FetchMe.Models.PhotographerModels;
using FetchMe.Models.PhotographTagModels;
using FetchMe.Models.PostModels;
using System;
using System.Collections.Generic;

namespace FetchMe.Models.PhotographModels
{
    public class Photograph : PhotographBase
    {
        public Guid PhotographerId { get; set; }
        public virtual Photographer Photographer { get; set; }

        // 1 to 1

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
        public virtual List<GalleryImage> Galleries { get; set; }
        public virtual List<Blog> Blogs { get; set; }
        public virtual List<Post> Posts { get; set; }

    }
}
