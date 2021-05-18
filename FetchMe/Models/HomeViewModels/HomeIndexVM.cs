using FetchMe.Models.PhotographerModels;
using FetchMe.Models.PhotographModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.HomeViewModels
{
    public class HomeIndexVM
    {
        public List<PhotographerIndexViewModel> Photographers { get; set; }

        public List<PhotographIndexViewModel> Photographs { get; set; }
    }
}
