using AutoMapper;
using FetchMe.Data;
using FetchMe.Models;
using FetchMe.Models.PostModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Services.PostServices
{
    public class PostService : IPostService
    {
        
        private readonly ILogger<PostService> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public PostService(
            ILogger<PostService> logger,
            ApplicationDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        public async Task<PostVM> CreatePostAsync(PostCreateVM postVM)
        {
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                if (!_httpContextAccessor.HttpContext.User.IsInRole("Admin")) return null;
                string userId = user.Id;

                postVM.OwnerId = userId;
                Post Post = _mapper.Map<Post>(postVM);

                Post.Created = DateTime.Now;

                await _context.Posts.AddAsync(Post);
                await _context.SaveChangesAsync();

                PostVM result = _mapper.Map<PostVM>(Post);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize]
        public async Task<PostVM> GetPostDetailsAsync(string slug)
        {
            try
            {
                Post Post = await _context.Posts
                    .FirstOrDefaultAsync(x => x.Slug == slug);

                PostVM PostVM = _mapper.Map<PostVM>(Post);

                return PostVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [Authorize]
        public async Task<PostIndexVM> GetPostsAsync(PostSearch search)
        {
            try
            {
                IQueryable<Post> PostQ = _context.Posts
                    .Include(x => x.Thumbnail);

                List<Post> Post = await PostQ.ToListAsync();

                PostIndexVM Posts = new PostIndexVM()
                {
                    Search = search,
                    Posts = _mapper.Map<List<PostIndexItemVM>>(Post)
                };

                return Posts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        public async Task<PostVM> UpdatePostAsync(PostUpdateVM PostVM)
        {
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                string userId = user.Id;

                Post Post = await _context.Posts.FirstOrDefaultAsync(x => x.Slug == PostVM.Slug);
                if (Post == null || Post.OwnerId != userId)
                {
                    return null;
                }

                Post = _mapper.Map(PostVM, Post);

                Post.LastModified = DateTime.Now;

                _context.Posts.Update(Post);
                await _context.SaveChangesAsync();

                PostVM result = _mapper.Map<PostVM>(Post);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
