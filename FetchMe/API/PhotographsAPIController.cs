using System.Collections.Generic;
using System.Threading.Tasks;
using FetchMe.Models.PhotographModels;
using FetchMe.Services.PhotographServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FetchMe.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotographsAPIController : ControllerBase
    {


        private readonly IPhotographService _photographService;

        public PhotographsAPIController(
            IPhotographService photographService
            )
        {
            _photographService = photographService;
        }

        // GET: api/<PhotographsAPIController>
        [HttpGet]
        public async Task<IEnumerable<PhotographIndexViewModel>> GetAsync()
        {
            PhotographSearch search = new PhotographSearch();
            List<PhotographIndexViewModel> photographerDtos = await _photographService.GetAllAsync(search);
            return photographerDtos;
        }

        // GET api/<PhotographsAPIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
