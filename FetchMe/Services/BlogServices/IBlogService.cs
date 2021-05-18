using FetchMe.Models.BlogModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Services.BlogServices
{
    public interface IBlogService
    {
        public Task<BlogIndexVM> GetBlogsAsync(BlogSearch search);
        public Task<BlogVM> GetBlogDetailsAsync(string Slug);
        public Task<BlogVM> CreateBlogAsync(BlogCreateVM blog);
        public Task<BlogVM> UpdateBlogAsync(BlogUpdateVM blog);
    }
}
