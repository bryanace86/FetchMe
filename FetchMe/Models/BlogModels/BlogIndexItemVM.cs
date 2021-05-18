using FetchMe.Models.PhotographModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.BlogModels
{
    public class BlogIndexItemVM
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public Guid ThumbnailId { get; set; }
        public virtual PhotographIndexViewModel Thumbnail { get; set; }
    }
}
