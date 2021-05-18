using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.SupportModels
{
    public class SupportTicketResponseCreate : AuditableModel
    {
        public string Response { get; set; }

        public string CreatedBy { get; set; }
        
        public Guid SupportTicketId { get; set; }

        public SupportTicketStatusVM Status { get; set; }
    }
}
