using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.UserModels
{
    public class ApplicationUserCompleteness
    {
        public string Email { get; set; }
        public bool HasSlug { get; set; }
        public bool HasPhotographer { get; set; }
        public bool HasImages { get; set; }
    }
}
