using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.SupportModels
{
    public class SupportTicketIndex : AuditableModel
    {
        public SupportTicketIndex()
        {
            SupportTicketCreate CreateTicket = new SupportTicketCreate();
            List<SupportTicketDetails> Tickets = new List<SupportTicketDetails>();
        }
        public SupportTicketCreate CreateTicket { get; set; }
        public List<SupportTicketDetails> Tickets { get; set; }

        public SupportTicketSearch Search { get; set; }
    }
}
