using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FetchMe.Models;
using FetchMe.Services.PhotographerServices;
using FetchMe.Services.PhotographServices;
using FetchMe.Models.PhotographerModels;
using FetchMe.Models.PhotographModels;
using FetchMe.Models.HomeViewModels;

namespace FetchMe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPhotographerService _photographerService;
        private readonly IPhotographService _photographService;

        public HomeController(
            ILogger<HomeController> logger,
            IPhotographerService photographerService,
            IPhotographService photographService
            )
        {
            _logger = logger;
            _photographerService = photographerService;
            _photographService = photographService;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            //return View();
            //get photographers
            //var PhotographersCtrl = new PhotographersController(_context, _appEnvironment, _config, _photographService, _mailChimp, _mapper, _httpContextAccessor, _emailSender, _locations);
            var photographers = await _photographerService.GetAllAsync(new PhotographerSearch());
            var photographs = await _photographService.GetAllAsync(new PhotographSearch());

            HomeIndexVM vm = new HomeIndexVM()
            {
                Photographers = photographers,
                Photographs = photographs
            };

            return View(vm);
        }

        [Route("[controller]/[action]")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("[controller]/[action]")]
        public IActionResult TermsAndConditions()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
