using FetchMe.Models.PhotographModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.LikeModels
{
    public class PhotographLike
    {
        public Guid Id { get; set; }

        public String UserId { get; set; }

        public Guid PhotographId { get; set; }
        public virtual Photograph Photograph { get; set; }
    }
}
