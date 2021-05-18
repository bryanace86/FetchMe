using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.SupportModels
{
    public class SupportTicket : AuditableModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedToName { get; set; }

        public SupportTicketStatus Status {get;set;}

        public Guid SupportTicketStatusId { get; set; }
        
        public List<SupportTicketResponse> Responses { get; set; }

    }
}
