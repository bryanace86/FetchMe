using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographerModels
{
    public class PhotographerSearchResults
    {
        public PhotographerSearch Search { get; set; }
        public List<PhotographerIndexViewModel> Photographers { get; set; }
    }
}
