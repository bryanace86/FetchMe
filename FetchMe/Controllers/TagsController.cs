using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FetchMe.Data;
using FetchMe.Models.TagModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FetchMe.Controllers
{
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TagsController(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet()]
        [Route("api/[controller]/[action]")]
        public Select2Tags GetPhotographTags([FromQuery] Select2Query query)
        {
            Select2Tags select2Tags = new Select2Tags()
            {
                Results = _mapper.Map<List<Select2Tag>>(_context.PhotographTags.Where(x => x.Id.Contains(query.Term)))
            };
            return select2Tags;
        }

        [HttpGet("{query}")]
        [Route("api/[controller]/[action]")]
        public Select2Tags GetPhotographerNames([FromQuery] Select2Query query)
        {
            Select2Tags select2Tags = new Select2Tags()
            {
                Results = _mapper.Map<List<Select2Tag>>(_context.Photographers.Where(x => x.DisplayName.Contains(query.Term)))
            };
            return select2Tags;
        }

        /*
        [HttpGet("{query}")]
        [Route("api/[controller]/[action]")]
        public Select2Tags GetGigTags([FromQuery] Select2Query query)
        {
            Select2Tags select2Tags = new Select2Tags()
            {
                Results = _mapper.Map<List<Select2Tag>>(_context.GigTags.Where(x => x.Id.Contains(query.Term)))
            };
            return select2Tags;
        }

        */





        [HttpGet()]
        [Route("api/[controller]/[action]")]
        public Select2Tags GetFStopTags([FromQuery] Select2Query query)
        {
            Select2Tags select2Tags = new Select2Tags()
            {
                Results = _mapper.Map<List<Select2Tag>>(_context.FStops.Where(x => x.Id.Contains(query.Term)))
            };
            return select2Tags;
        }

        [HttpGet()]
        [Route("api/[controller]/[action]")]
        public Select2Tags GetExposureTimeTags([FromQuery] Select2Query query)
        {
            Select2Tags select2Tags = new Select2Tags()
            {
                Results = _mapper.Map<List<Select2Tag>>(_context.ExposureTimes.Where(x => x.Id.Contains(query.Term)))
            };
            return select2Tags;
        }

        [HttpGet()]
        [Route("api/[controller]/[action]")]
        public Select2Tags GetISOTags([FromQuery] Select2Query query)
        {
            Select2Tags select2Tags = new Select2Tags()
            {
                Results = _mapper.Map<List<Select2Tag>>(_context.ISOs.Where(x => x.Id.Contains(query.Term)))
            };
            return select2Tags;
        }

        [HttpGet()]
        [Route("api/[controller]/[action]")]
        public Select2Tags GetFocalLengthTags([FromQuery] Select2Query query)
        {
            Select2Tags select2Tags = new Select2Tags()
            {
                Results = _mapper.Map<List<Select2Tag>>(_context.FocalLengths.Where(x => x.Id.Contains(query.Term)))
            };
            return select2Tags;
        }

        [HttpGet()]
        [Route("api/[controller]/[action]")]
        public Select2Tags GetCameraBodyTags([FromQuery] Select2Query query)
        {
            Select2Tags select2Tags = new Select2Tags()
            {
                Results = _mapper.Map<List<Select2Tag>>(_context.CameraBodies.Where(x => x.Id.Contains(query.Term)))
            };
            return select2Tags;
        }

        [HttpGet()]
        [Route("api/[controller]/[action]")]
        public Select2Tags GetLensTags([FromQuery] Select2Query query)
        {
            Select2Tags select2Tags = new Select2Tags()
            {
                Results = _mapper.Map<List<Select2Tag>>(_context.Lenses.Where(x => x.Id.Contains(query.Term)))
            };
            return select2Tags;
        }

        [HttpGet()]
        [Route("api/[controller]/[action]")]
        public Select2Tags GetLightSourceTags([FromQuery] Select2Query query)
        {
            Select2Tags select2Tags = new Select2Tags()
            {
                Results = _mapper.Map<List<Select2Tag>>(_context.LightSources.Where(x => x.Id.Contains(query.Term)))
            };
            return select2Tags;
        }
    }
}
