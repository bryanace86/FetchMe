using FetchMe.Models.PhotographModels;
using System.Collections.Generic;

namespace FetchMe.Models.PhotographTagModels
{
    public class PhotographTag
    {
        public string Id { get; set; }
        public virtual List<Photograph> Photographs { get; set; }
    }
}
