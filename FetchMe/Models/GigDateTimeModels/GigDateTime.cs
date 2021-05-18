using FetchMe.Models.DateTimeSpanModels;
using FetchMe.Models.GigModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.GigDateTimeModels
{
    public class GigDateTime
    {
        public Guid Id { get; set; }

        public Guid GigId { get; set; }
        public virtual Gig Gig { get; set; }

        public Guid DateTimeSpanId { get; set; }
        public virtual DateTimeSpan DateTimeSpan { get; set; }
    }
}
