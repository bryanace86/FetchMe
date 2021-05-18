using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.SupportModels
{
    public class SupportTicketSearch
    {
        public Guid Status { get; set; }
        public int OrderBy { get; set; }
    }
}
