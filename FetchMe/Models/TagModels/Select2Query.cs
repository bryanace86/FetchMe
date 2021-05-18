using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.TagModels
{
    public class Select2Query
    {
        public string Term { get; set; }
        public string Q { get; set; }
        public string _Type { get; set; }
        public string Page { get; set; }
    }
}
