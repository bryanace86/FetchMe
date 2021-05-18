using FetchMe.Models.SupportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Services.SupportServices
{
    public interface ISupportService
    {
        Task<SupportTicketIndex> GetUsersSupportTickets(SupportTicketSearch search);
        Task<SupportTicketDetails> DetailsAsync(Guid id);
        Task<SupportTicketDetails> Create(SupportTicketIndex supportTicketIndex);
        Task<SupportTicketResponseDetails> CreateResponse(SupportTicketResponseCreate resp);
        Task<List<SupportTicketStatus>> GetSupportTicketStatusesAsync();
        Task<bool> ChangeStatus(SupportStatusUpdateVM updateVM);
    }
}
