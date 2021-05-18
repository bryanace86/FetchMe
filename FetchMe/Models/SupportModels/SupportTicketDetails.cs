using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.SupportModels
{
    public class SupportTicketDetails : AuditableModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }

        public List<SupportTicketStatusVM> AvailableStatus { get; set; }

        public SupportTicketStatusVM Status { get; set; }

        public ICollection<SupportTicketResponseDetails> Responses { get; set; }

        public bool HasNotViewed { get; set; }

        public SupportTicketResponseCreate Response { get; set; }
    }
}
