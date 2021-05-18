using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FetchMe.Models.LikeModels;
using FetchMe.Services.LikeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FetchMe.Controllers
{
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }


        [Authorize]
        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> PostLike(AlterLikeDto like)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = await _likeService.CreateLike(like);

            if (result) {
                return Ok();
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        //Delete: api/PhotographLikes
        [Authorize]
        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> DeleteLike(AlterLikeDto like)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = await _likeService.DeleteLike(like);

            if (result)
            {
                return Ok();
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
