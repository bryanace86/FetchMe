using FetchMe.Models.PhotographModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PostModels
{
    public class PostIndexItemVM : AuditableModel
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid ThumbnailId { get; set; }
        public virtual PhotographIndexViewModel Thumbnail { get; set; }
        public string ShortDescription { get; set; }
        public Guid BlogId { get; set; }
    }
}
