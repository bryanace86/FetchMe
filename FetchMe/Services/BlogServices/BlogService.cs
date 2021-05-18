using AutoMapper;
using FetchMe.Data;
using FetchMe.Models;
using FetchMe.Models.BlogModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace FetchMe.Services.BlogServices
{
    public class BlogService : IBlogService
    {
        private readonly ILogger<BlogService> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public BlogService(
            ILogger<BlogService> logger,
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
        public async Task<BlogVM> CreateBlogAsync(BlogCreateVM blogVM)
        {
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                if (!_httpContextAccessor.HttpContext.User.IsInRole("Admin")) return null;
                string userId = user.Id;

                blogVM.OwnerId = userId;
                Blog blog = _mapper.Map<Blog>(blogVM);

                blog.Created = DateTime.Now;

                await _context.Blogs.AddAsync(blog);
                await _context.SaveChangesAsync();

                BlogVM result = _mapper.Map<BlogVM>(blog);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        public async Task<BlogVM> GetBlogDetailsAsync(string slug)
        {
            try
            {
                Blog blog = await _context.Blogs
                    .Include(x => x.Posts)
                        .ThenInclude(x => x.Thumbnail)
                    .FirstOrDefaultAsync(x => x.Slug == slug);

                BlogVM blogVM = _mapper.Map<BlogVM>(blog);

                return blogVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [Authorize]
        public async Task<BlogIndexVM> GetBlogsAsync(BlogSearch search)
        {
            try
            {
                IQueryable<Blog> blogQ = _context.Blogs
                    .Include(x => x.Thumbnail);

                List<Blog> blog = await blogQ.ToListAsync();

                BlogIndexVM blogs = new BlogIndexVM()
                {
                    Search = search,
                    Blogs = _mapper.Map<List<BlogIndexItemVM>>(blog)
                };
                
                return blogs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        public async Task<BlogVM> UpdateBlogAsync(BlogUpdateVM blogVM)
        {
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                string userId = user.Id;

                Blog blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Slug == blogVM.Slug);
                if (blog == null || blog.OwnerId != userId)
                {
                    return null;
                }

                blog = _mapper.Map(blogVM, blog);

                blog.LastModified = DateTime.Now;

                _context.Blogs.Update(blog);
                await _context.SaveChangesAsync();

                BlogVM result = _mapper.Map<BlogVM>(blog);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
