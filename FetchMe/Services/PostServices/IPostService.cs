using FetchMe.Models.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Services.PostServices
{
    public interface IPostService
    {
        Task<PostIndexVM> GetPostsAsync(PostSearch search);
        Task<PostVM> GetPostDetailsAsync(string Slug);
        Task<PostVM> CreatePostAsync(PostCreateVM blog);
        Task<PostVM> UpdatePostAsync(PostUpdateVM blog);
    }
}
