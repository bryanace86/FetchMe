using FetchMe.Models.PhotographModels;
using System.Collections.Generic;

namespace FetchMe.Models.PhotographTagModels
{
    public class PhotographTags
    {
        public string Id { get; set; }
        public virtual List<PhotographTagsPhotograph> TagPhotographs { get; set; }
    }
}
