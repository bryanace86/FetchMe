using FetchMe.Models.GalleryModels;
using FetchMe.Models.PhotographModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.UserModels
{
    public class ApplicationUserDetailsVm
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Slug")]
        [StringLength(150)]
        [RegularExpression("^[a-zA-Z0-9-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [Remote("IsSlugUnique", "UserProfileAPI", ErrorMessage = "This {0} is already used.")]
        public string Slug { get; set; }

        [Display(Name = "First Name")] 
        public string FirstName { get; set; }

        [Display(Name = "Last Name")] 
        public string LastName { get; set; }

        [Display(Name = "Display Name")] 
        public string DisplayName { get; set; }

        [Display(Name = "Profile Image")] 
        public UserProfileImageDto UserProfileImage { get; set; }
        public bool IsOwner { get; set; }

        public GalleryIndexVM Galleries { get; set; }
    }
}
