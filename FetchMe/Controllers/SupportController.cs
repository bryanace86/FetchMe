using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FetchMe.Models.SupportModels;
using FetchMe.Services.SupportServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FetchMe.Controllers
{
    public class SupportController : Controller
    {
        private readonly ISupportService _supportService;

        public SupportController(ISupportService supportService)
        {
            _supportService = supportService;

        }

        [Authorize]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> IndexAsync(SupportTicketSearch search)
        {
            SupportTicketIndex indexVM = await _supportService.GetUsersSupportTickets(search);


            ViewBag.AvailableStatuses = new SelectList(await _supportService.GetSupportTicketStatusesAsync(), "Id", "Status", search.Status);


            List<KeyValuePair<string, string>> availableOrdinals = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string,string>("1", "Date Created Ascending"),
                new KeyValuePair<string,string>("2", "Date Created Descending")

            };
            ViewBag.AvailableOrdinals = new SelectList(availableOrdinals, "Key", "Value", search.OrderBy.ToString());

            return View(indexVM);
        }

        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public async Task<IActionResult> DetailsAsync(Guid id)
        {

            SupportTicketDetails supportTicketVM = await _supportService.DetailsAsync(id);

            ViewBag.AvailableStatuses = new SelectList(await _supportService.GetSupportTicketStatusesAsync(), "Id", "Status", supportTicketVM.Status.Id);

            return PartialView("~/Views/Support/Details.cshtml", supportTicketVM);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]/")]
        public async Task<IActionResult> Create(SupportTicketIndex supportTicketIndex)
        {
            SupportTicketDetails supportTicketDetails = await _supportService.Create(supportTicketIndex);

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> CreateResponse(SupportTicketResponseCreate resp)
        {
            SupportTicketResponseDetails supportTicketDetails = await _supportService.CreateResponse(resp);
            return PartialView("~/Views/Support/ResponseDetails.cshtml", supportTicketDetails);
        }

        [HttpPost("[controller]/[action]")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(SupportStatusUpdateVM updateVM)
        {
            bool result = await _supportService.ChangeStatus(updateVM);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
