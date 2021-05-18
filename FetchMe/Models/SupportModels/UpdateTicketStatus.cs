using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.SupportModels
{
    public class UpdateTicketStatus
    {
        public Guid TicketId { get; set; }
        public Guid StatusId { get; set; }

    }
}
