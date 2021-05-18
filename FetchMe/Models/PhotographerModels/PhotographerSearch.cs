using FetchMe.Models.LocationModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographerModels
{
    public class PhotographerSearch
    {
        public Location Location { get; set; }

        public int Distance { get; set; }

        [Display(Name = "Tags")]
        public List<string> PhotographTagIds { get; set; }

        [Display(Name = "Display Name")]
        public List<string> PhotographerName { get; set; }

        [Display(Name = "Show Favorites")]
        public bool ShowFavorites { get; set; }

        [Display(Name = "Insured")]
        public bool IsInsured { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string Slug { get; set; }

        [Display(Name = "Years Of Experience")]
        public int YearsExperience { get; set; }
    }
}
