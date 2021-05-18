using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FetchMe.Models.PhotographerModels;
using FetchMe.Models.PhotographModels;
using FetchMe.Services.PhotographerServices;
using FetchMe.Services.PhotographServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FetchMe.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotographersAPIController : ControllerBase
    {
        private readonly IPhotographerService _photographerService;
        private readonly IPhotographService _photographService;

        public PhotographersAPIController(
            IPhotographerService photographerService,
            IPhotographService photographService
            )
        {
            _photographerService = photographerService;
            _photographService = photographService;
        }

        // GET: api/<PhotographersAPIController>
        [HttpGet]
        public async Task<PhotographerSearchResults> GetAsync()
        {
            PhotographerSearch search = new PhotographerSearch();
            if (search.PageSize == 0) search.PageSize = 10;

            IEnumerable<PhotographerIndexViewModel> photographerDtos = await _photographerService.GetAllAsync(search);
            /*
            foreach (PhotographerDto photographerDto in photographerDtos)
            {
                photographerDto.Photographs = await _photographService.GetAllAsync(new PhotographSearch()
                {
                    PhotographerSlug = photographerDto.Slug,
                    PhotographTagIds = search.PhotographTagIds,
                });
            }
            */
            PhotographerSearchResults photogrpherResults = new PhotographerSearchResults()
            {
                Search = search,
                Photographers = photographerDtos.ToList()
            };
            return photogrpherResults;
        }

        // GET api/<PhotographersAPIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PhotographersAPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PhotographersAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PhotographersAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
