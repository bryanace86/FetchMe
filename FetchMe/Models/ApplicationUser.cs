using FetchMe.Data.Migrations;
using FetchMe.Models.BlogModels;
using FetchMe.Models.GalleryModels;
using FetchMe.Models.PhotographerModels;
using FetchMe.Models.PhotographModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FetchMe.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Slug { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        
        public Guid UserProfileImageId { get; set; }
        public UserProfileImage UserProfileImage { get; set; }

        public Guid UserBannerImageId { get; set; }
        public UserBannerImage UserBannerImage { get; set; }

        public virtual IEnumerable<Gallery> Galleries { get; set; }
        public virtual IEnumerable<Blog> Blogs { get; set; }

        public virtual Photographer Photographer { get; set; }
    }

}
