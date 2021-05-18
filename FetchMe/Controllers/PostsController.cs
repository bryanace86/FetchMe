using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FetchMe.Models.PostModels;
using FetchMe.Services.PostServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vereyon.Web;

namespace FetchMe.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly IFlashMessage _flashMessage;

        public PostsController(
            IPostService postService,
            IFlashMessage flashMessage
            )
        {
            _postService = postService;
            _flashMessage = flashMessage;
        }

        //Gets an index of all posts
        [HttpGet("[controller]/[action]")]
        public async Task<IActionResult> IndexAsync(PostSearch search)
        {
            if (!ModelState.IsValid) return View();

            PostIndexVM postIndexVM = await _postService.GetPostsAsync(search);

            return View(postIndexVM);
        }

        //Gets a single Posts details page
        [HttpGet("[controller]/[action]/{slug}")]
        public async Task<IActionResult> DetailsAsync(string slug)
        {
            PostVM postVM = await _postService.GetPostDetailsAsync(slug);

            return View(postVM);
        }

        //Creates a single Post
        [HttpPost("[controller]/[action]")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(PostCreateVM PostVM)
        {
            if (!ModelState.IsValid) return View(PostVM);

            PostVM post = await _postService.CreatePostAsync(PostVM);
            if (post == null) return RedirectToAction("Index");
            _flashMessage.Info("You new Post has been created");

            return RedirectToAction("Details", new { slug = post.Slug });
        }

        //Update a single Post
        //Creates a single Post
        [HttpPost("[controller]/[action]")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(PostUpdateVM PostVM)
        {
            if (!ModelState.IsValid) return View(PostVM);

            PostVM post = await _postService.UpdatePostAsync(PostVM);
            if (post == null) return RedirectToAction("Index");
            _flashMessage.Info("You new Post has been created");

            return RedirectToAction("Details", new { slug = post.Slug });
        }
    }
}
