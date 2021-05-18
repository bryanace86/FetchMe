using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographModels
{
    public class PhotographSearchResults
    {
        public PhotographSearch Search { get; set; }
        public List<PhotographIndexViewModel> Photographs { get; set; }
    }
}
