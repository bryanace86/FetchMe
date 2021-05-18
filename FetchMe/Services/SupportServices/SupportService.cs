using AutoMapper;
using FetchMe.Data;
using FetchMe.Models;
using FetchMe.Models.SupportModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace FetchMe.Services.SupportServices
{
    public class SupportService : ISupportService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly string _userId;
        private readonly string _userName;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;

        public SupportService(
            ApplicationDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContext,
            IEmailSender emailSender,
             UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
            if (httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                _userId = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _userName = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            _emailSender = emailSender;
            _userManager = userManager;
        }

        public async Task<SupportTicketIndex> GetUsersSupportTickets(SupportTicketSearch search)
        {
            SupportTicketIndex indexVM = new SupportTicketIndex();
            IQueryable<SupportTicket> ticketsQ = _context.SupportTickets
                    .Include(x => x.Status)
                    .Include(x => x.Responses);

            if (_userId != null)
            {

                if (_httpContext.HttpContext.User.IsInRole("Support"))
                {
                    ticketsQ = ticketsQ
                    .Where(x => x.AssignedTo == _userId || string.IsNullOrWhiteSpace(x.AssignedTo));

                }
                else
                {
                    ticketsQ = ticketsQ
                    .Where(x => x.CreatedBy == _userId);
                }

            }

            if (search.Status != Guid.Empty)
            {
                ticketsQ = ticketsQ
                    .Where(x => x.SupportTicketStatusId == search.Status);
            }

            int ordinal = (search.OrderBy == 0) ? 1 : search.OrderBy;
            if (ordinal == 1)
            {
                ticketsQ = ticketsQ.OrderByDescending(x => x.Created);
            }
            else
            {
                ticketsQ = ticketsQ.OrderBy(x => x.Created);
            }

            List<SupportTicket> tickets = await ticketsQ
                    .ToListAsync();

            indexVM.Tickets = _mapper.Map<List<SupportTicketDetails>>(tickets);
            foreach (SupportTicketDetails ticket in indexVM.Tickets)
            {
                ticket.HasNotViewed = ticket.Responses.Any(x => x.Viewed == false && x.CreatedBy != _userId);
            }

            return indexVM;
        }


        public async Task<SupportTicketDetails> DetailsAsync(Guid id)
        {

            SupportTicket supportTicket = _context.SupportTickets
                .Include(x => x.Status)
                .IncludeFilter(x => x.Responses
                    .OrderByDescending(y => y.Created)
                    .Take(500))
                .FirstOrDefault(x => x.Id == id);

            List<SupportTicketResponse> supportTicketResponses = _context.SupportTicketResponses
                .Where(x => x.SupportTicketId == id && x.Viewed == false && x.CreatedBy != _userId).ToList();
            foreach (SupportTicketResponse response in supportTicketResponses)
            {
                response.Viewed = true;
            }
            
            await _context.SaveChangesAsync();

            SupportTicketDetails supportTicketVM = _mapper.Map<SupportTicketDetails>(supportTicket);
            supportTicketVM.HasNotViewed = supportTicketVM.Responses.Any(x => x.Viewed == false && x.CreatedBy != _userId);

            return supportTicketVM;
        }


        [Authorize]
        public async Task<SupportTicketDetails> Create(SupportTicketIndex supportTicketIndex)
        {
            SupportTicket supportTicket = _mapper.Map<SupportTicket>(supportTicketIndex.CreateTicket);
            supportTicket.CreatedBy = _userId;
            supportTicket.CreatedByName = _userName;
            supportTicket.Created = DateTime.Now;
            supportTicket.Status = await _context.SupportTicketStatus.FirstOrDefaultAsync(x => x.Status == "New");

            _context.Add(supportTicket);
            await _context.SaveChangesAsync();

            await _emailSender.SendEmailAsync("bryanace86@gmail.com", "A new support ticket awaits", "go fix it");

            SupportTicketDetails supportTicketDetails = _mapper.Map<SupportTicketDetails>(supportTicket);

            return supportTicketDetails;

        }


        [Authorize]
        public async Task<SupportTicketResponseDetails> CreateResponse(SupportTicketResponseCreate resp)
        {
            SupportTicketResponse supportTicketResponse = _mapper.Map<SupportTicketResponse>(resp);
            supportTicketResponse.CreatedBy = _userId;
            supportTicketResponse.CreatedByName = _userName;
            supportTicketResponse.Created = DateTime.Now;

            /*
            if (resp.Status != null)
            {
                SupportTicket supportTicket = await _context.SupportTickets.FirstOrDefaultAsync(x => x.Id == resp.SupportTicketId);

                if (resp.Status.Id != supportTicket.SupportTicketStatusId || resp.Status.Id == null)
                {
                    supportTicket.SupportTicketStatusId = resp.Status.Id;
                }
            }
            */

            SupportTicket supportTicket = await _context.SupportTickets.FirstOrDefaultAsync(x => x.Id == resp.SupportTicketId);
            if (_httpContext.HttpContext.User.IsInRole("Support"))
            {
                supportTicket.SupportTicketStatusId = _context.SupportTicketStatus.FirstOrDefault(x => x.Status == "Pending User Response").Id;

                ApplicationUser user = await _userManager.FindByIdAsync(supportTicket.CreatedBy);
                await _emailSender.SendEmailAsync(user.Email, "A support ticket is ready for a response", "A support member has responded to your ticket.");
            }
            else
            {
                supportTicket.SupportTicketStatusId = _context.SupportTicketStatus.FirstOrDefault(x => x.Status == "Pending Support Response").Id;

                await _emailSender.SendEmailAsync("bryanace86@gmail.com", "A new ticked has been responded to", "go fix it");
            }

            _context.Add(supportTicketResponse);
            _context.SupportTickets.Update(supportTicket);
            await _context.SaveChangesAsync();

            SupportTicketResponseDetails response = _mapper.Map<SupportTicketResponseDetails>(supportTicketResponse);

            return response;
        }
    
        public async Task<List<SupportTicketStatus>> GetSupportTicketStatusesAsync()
        {
            List<SupportTicketStatus> statuses = await _context.SupportTicketStatus.ToListAsync();
            return statuses;
        }
    
        public async Task<bool> ChangeStatus(SupportStatusUpdateVM updateVM)
        {

            try
            {
                if (!_httpContext.HttpContext.User.IsInRole("Support"))
                {
                    return false;
                }

                SupportTicket ticket = await _context.SupportTickets.Include(x => x.Status).FirstOrDefaultAsync(x => x.Id == updateVM.TicketId);
                ticket.SupportTicketStatusId = updateVM.Status;

                _context.SupportTickets.Update(ticket);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
