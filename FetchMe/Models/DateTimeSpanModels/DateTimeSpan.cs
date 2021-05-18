using FetchMe.Models.GigDateTimeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.DateTimeSpanModels
{
    public class DateTimeSpan
    {
        public Guid Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public virtual List<GigDateTime> GigDateTimes { get; set; }
    }
}
