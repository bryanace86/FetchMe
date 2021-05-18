using FetchMe.Models.PhotographModels;
using FetchMe.Models.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.BlogModels
{
    public class BlogVM : AuditableModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public PostIndexVM PostIndex { get; set; }
        public Guid ThumbnailId { get; set; }
        public virtual PhotographIndexViewModel Thumbnail { get; set; }
    }
}
