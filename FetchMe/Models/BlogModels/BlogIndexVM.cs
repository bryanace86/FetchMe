using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.BlogModels
{
    public class BlogIndexVM
    {
        public BlogSearch Search { get; set; }
        public List<BlogIndexItemVM> Blogs { get; set; }
    }
}
