using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.SupportModels
{
    public class SupportTicketStatus
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public ICollection<SupportTicket> Tickets { get; set; }
    }
}
