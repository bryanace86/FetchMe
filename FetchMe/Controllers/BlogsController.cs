using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FetchMe.Models.BlogModels;
using FetchMe.Services.BlogServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vereyon.Web;

namespace FetchMe.Controllers
{
    public class BlogsController : Controller
    {

        private readonly IBlogService _blogService;
        private readonly IFlashMessage _flashMessage;

        public BlogsController(
            IBlogService blogService,
            IFlashMessage flashMessage
            )
        {
            _blogService = blogService;
            _flashMessage = flashMessage;
        }

        //Gets an index of all blogs
        [HttpGet("[controller]/[action]")]
        public async Task<IActionResult> IndexAsync(BlogSearch search)
        {
            if (!ModelState.IsValid) return View();

            BlogIndexVM blogIndexVM = await _blogService.GetBlogsAsync(search);

            return View(blogIndexVM);
        }

        //Gets a single blogs details page
        [HttpGet("[controller]/[action]/{slug}")]
        public async Task<IActionResult> DetailsAsync(string slug)
        {
            BlogVM blogVM = await _blogService.GetBlogDetailsAsync(slug);
            
            return View(blogVM);
        }

        //Creates a single blog
        [HttpPost("[controller]/[action]")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(BlogCreateVM blogVM)
        {
            if (!ModelState.IsValid) return View(blogVM);

            BlogVM blog = await _blogService.CreateBlogAsync(blogVM);
            if (blog == null) return RedirectToAction("Index");
            _flashMessage.Info("Your new blog has been created");

            return RedirectToAction("Details", new { slug = blog.Slug });
        }

        //Update a single blog
        [HttpPost("[controller]/[action]")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(BlogUpdateVM blogVM)
        {
            if (!ModelState.IsValid) return View(blogVM);

            BlogVM blog = await _blogService.UpdateBlogAsync(blogVM);
            if (blog == null) return RedirectToAction("Index");
            _flashMessage.Info("Your blog has been updated");

            return RedirectToAction("Details", new { slug = blog.Slug });
        }
        public IActionResult Updated()
        {
            return View();
        }
    }
}
