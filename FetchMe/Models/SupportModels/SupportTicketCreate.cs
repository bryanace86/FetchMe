using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.SupportModels
{
    public class SupportTicketCreate : AuditableModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public string CreatedBy { get; set; }

        public SupportTicketStatusVM Status { get; set; }
    }
}
