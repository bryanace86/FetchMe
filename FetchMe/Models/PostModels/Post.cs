using FetchMe.Models.BlogModels;
using FetchMe.Models.PhotographModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PostModels
{
    public class Post : AuditableModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }
        public string ShortDescription { get; set; }
        public Guid? ThumbnailId { get; set; }
        public virtual Photograph Thumbnail { get; set; }
        public Guid BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public string OwnerId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
