using FetchMe.Models.PhotographModels;
using FetchMe.Models.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.BlogModels
{
    public class Blog : AuditableModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<Post> Posts { get; set; }

        public Guid? ThumbnailId { get; set; }
        public virtual Photograph Thumbnail { get; set; }

        public string OwnerId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
