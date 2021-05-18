using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PostModels
{
    public class PostIndexVM
    {
        public PostSearch Search { get; set; }
        public virtual IEnumerable<PostIndexItemVM> Posts { get; set; }
    }
}
