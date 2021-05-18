using FetchMe.Models.GalleryModels;
using FetchMe.Models.LikeModels;
using FetchMe.Models.PhotographerBadgeModels;
using FetchMe.Models.PhotographerLocationModels;
using FetchMe.Models.PhotographModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FetchMe.Models.PhotographerModels
{
    public class PhotographerDetailsDto
    {
        public Guid Id { get; set; }

        [StringLength(150)]
        [RegularExpression("^[a-zA-Z0-9-]*$", ErrorMessage = "Only Letters and Numbers allowed.")]
        public string Slug { get; set; }
        // user ID from AspNetUser table.
        public string OwnerID { get; set; }

        [StringLength(150)]
        public string DisplayName { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [StringLength(280)]
        public string CardExcerpt { get; set; }

        [StringLength(3000)]
        public string ProfileBio { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime YearStarted { get; set; }

        public string PhoneNumber { get; set; }

        public string WebSite { get; set; }

        public string Instagram { get; set; }

        public string Facebook { get; set; }

        public string Pinterest { get; set; }

        public string Twitter { get; set; }

        public string Tumblr { get; set; }

        public bool IsInsured { get; set; }

        public Guid? BannerImageId { get; set; }

        public virtual Photograph BannerImage { get; set; }

        public Guid? LogoImageId { get; set; }

        public virtual Photograph LogoImage { get; set; }

        public virtual List<PhotographIndexViewModel> Photographs { get; set; }

        public virtual List<PhotographerLocation> Locations { get; set; }

        public virtual List<PhotographerLike> PhotographerLikes { get; set; }
        public virtual List<PhotographerBadge> PhotographerBadges { get; set; }

        public GalleryIndexVM GalleryIndex { get; set; }

        public bool IsLiked { get; set; }

        public PhotographSearch PhotographSearch { get; set; }
        public bool IsOwner { get; set; }
    }
}
