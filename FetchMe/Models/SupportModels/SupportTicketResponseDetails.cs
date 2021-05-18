using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.SupportModels
{
    public class SupportTicketResponseDetails : AuditableModel
    {
        public Guid Id { get; set; }
        public string Response { get; set; }

        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }

        public Guid SupportTicketId { get; set; }

        public bool Viewed { get; set; }
    }
}
