using FetchMe.Models.PhotographModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographTagModels
{
    public class PhotographTagsPhotograph
    {
        public Guid PhotographId { get; set; }
        public Photograph Photograph { get; set; }

        public string PhotographTagId { get; set; }
        public PhotographTags PhotographTag { get; set; }
    }
}
