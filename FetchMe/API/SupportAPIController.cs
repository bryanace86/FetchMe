using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FetchMe.Models.SupportModels;
using FetchMe.Services.SupportServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FetchMe.API
{
    public class SupportAPIController : Controller
    {
        private readonly ISupportService _supportService;

        public SupportAPIController(ISupportService supportService)
        {
            _supportService = supportService;

        }

        [HttpPost("[controller]/[action]")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(SupportStatusUpdateVM updateVM)
        {
            bool result = await _supportService.ChangeStatus(updateVM);
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
