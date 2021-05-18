using FetchMe.Models.PhotographerBadgeModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FetchMe.Models.PhotographerModels
{
    public class PhotographerUpdateDto
    {
        public Guid Id { get; set; }
        
        [Required]
        [Display(Name = "Slug")]
        [StringLength(150)]
        [RegularExpression("^[a-zA-Z0-9-]*$", ErrorMessage = "Only Letters and Numbers allowed.")]
        [Remote("IsSlugUnique", "Photographers", /*AdditionalFields = "Id",*/ ErrorMessage = "This {0} is already used.")]
        public string Slug { get; set; }
        // user ID from AspNetUser table.
        public string OwnerID { get; set; }

        public string OriginalSlug { get; set; }

        [StringLength(150)]
        [Display(Name = "Display Name")]

        public string DisplayName { get; set; }
        
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]

        public string LastName { get; set; }

        [StringLength(280)]
        [Display(Name = "Card Excerpt")]

        public string CardExcerpt { get; set; }

        [StringLength(3000)]
        [Display(Name = "Profile Bio")]

        public string ProfileBio { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime YearStarted { get; set; }

        [Display(Name = "Phone Number")]

        public string PhoneNumber { get; set; }

        public string WebSite { get; set; }

        public string Instagram { get; set; }

        public string Facebook { get; set; }

        public string Pinterest { get; set; }

        public string Twitter { get; set; }

        public string Tumblr { get; set; }

        [Display(Name = "I am insured")]

        public bool IsInsured { get; set; }

        public virtual List<PhotographerBadge> PhotographerBadges { get; set; }
        public bool SubscribeToNewsletter { get; set; }
    }
}
