using FetchMe.Models.PhotographModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographerModels
{
    public class PhotographerIndexViewModel
    {
        public Guid Id { get; set; }
        public string Slug { get; set; }
        // user ID from AspNetUser table.
        public string OwnerID { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardExcerpt { get; set; }
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

        public Guid? LogoImageId { get; set; }

        public PhotographIndexViewModel LogoImage { get; set; }

        public virtual List<PhotographIndexViewModel> Photographs { get; set; }
        //public virtual List<PhotographerLike> PhotographerLikes { get; set; }
        //public virtual List<PhotographerBadge> PhotographerBadges { get; set; }

        public bool IsLiked { get; set; }

        public PhotographSearch PhotographSearch { get; set; }
        //public bool IsOwner { get; set; }
    }
}
