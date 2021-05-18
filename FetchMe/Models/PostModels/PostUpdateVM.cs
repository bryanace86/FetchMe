using FetchMe.Models.BlogModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PostModels
{
    public class PostUpdateVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }
        public string ShortDescription { get; set; }
        public Guid ThumbnailId { get; set; }
        public Guid BlogId { get; set; }
    }
}
