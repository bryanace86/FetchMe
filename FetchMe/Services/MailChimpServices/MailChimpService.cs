using MailChimp.Net;
using MailChimp.Net.Core;
using MailChimp.Net.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FetchMe.Services.MailChimpServices
{
    public class MailChimpService : IMailChimpService
    {
        private const string ApiKey = "stripped";
        private const string ListId = "stripped";
        private const int TemplateId = 9999;
        private MailChimpManager _mailChimpManager;
        private Setting _campaignSettings = new Setting
        {
            ReplyTo = "stripped",
            FromName = "FetchMe Photography",
            Title = "FetchMe Photography",
            SubjectLine = "The email subject",
        };


        private readonly IHttpContextAccessor _httpContextAccessor;

        public MailChimpService(
            IHttpContextAccessor httpContextAccessor
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _mailChimpManager = new MailChimpManager(ApiKey);
        }

        // `html` contains the content of your email using html notation
        public void CreateAndSendCampaign(string html)
        {
            var campaign = _mailChimpManager.Campaigns.AddAsync(new Campaign
            {
                Settings = _campaignSettings,
                Recipients = new Recipient { ListId = ListId },
                Type = CampaignType.Regular
            }).Result;
            var timeStr = DateTime.Now.ToString();
            var content = _mailChimpManager.Content.AddOrUpdateAsync(
                campaign.Id,
                new ContentRequest()
                {
                    Template = new ContentTemplate
                    {
                        Id = TemplateId,
                        Sections = new Dictionary<string, object>()
                        {
                            { "body_content", html },
                            { "preheader_leftcol_content", $"<p>{timeStr}</p>" }
                        }
                    }
                }).Result;
            _mailChimpManager.Campaigns.SendAsync(campaign.Id).Wait();
        }
        public List<Template> GetAllTemplates()
          => _mailChimpManager.Templates.GetAllAsync().Result.ToList();
        public List<List> GetAllMailingLists()
          => _mailChimpManager.Lists.GetAllAsync().Result.ToList();
        public Content GetTemplateDefaultContent(string templateId)
          => (Content)_mailChimpManager.Templates.GetDefaultContentAsync(templateId).Result;

        public async Task<IEnumerable<Member>> GetSubscribersAsync()
        {
            var listId = ListId;
            IEnumerable<Member> members = await _mailChimpManager.Members.GetAllAsync(listId).ConfigureAwait(false);
            return members;
        }

        public async Task<Member> GetSubscriberAsync()
        {
            var listId = ListId;

            var userEmail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            Member member = await _mailChimpManager.Members.GetAsync(listId, userEmail).ConfigureAwait(false);
            return member;
        }

        public async Task<bool> IsUserSubscribedAsync()
        {
            var listId = ListId;
            
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var result = await _mailChimpManager.Members.ExistsAsync(listId, userEmail, null, true).ConfigureAwait(false);
            return result;
        }

        public async Task<Member> UnsubscribeUserAsync()
        {
            var listId = ListId;
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

            // Use the Status property if updating an existing member
            var member = new Member { EmailAddress = userEmail, StatusIfNew = Status.Unsubscribed, Status = Status.Unsubscribed };
            return await _mailChimpManager.Members.AddOrUpdateAsync(listId, member);
        }

        public async Task<Member> UpsertSubscriberAsync()
        {
            try
            {
                var listId = ListId;
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userFirstName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.GivenName);
                var userLastName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Surname);

                var userEmail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

                // Use the Status property if updating an existing member
                var member = new Member { EmailAddress = userEmail, StatusIfNew = Status.Subscribed, Status = Status.Subscribed };
                //member.MergeFields.Add("FNAME", userFirstName);
                //member.MergeFields.Add("LNAME", userLastName);
                var result = await _mailChimpManager.Members.AddOrUpdateAsync(listId, member);
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<Member> UpdateSubscriber(string email)
        {
            try
            {
                // Get reference to existing user if you don't already have it
                var listId = ListId;
                var members = await _mailChimpManager.Members.GetAllAsync(listId).ConfigureAwait(false);
                var member = members.First(x => x.EmailAddress == email);

                // Update the user
                member.MergeFields.Add("FNAME", "New first name");
                member.MergeFields.Add("LNAME", "New last name");
                return await _mailChimpManager.Members.AddOrUpdateAsync(listId, member);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<Member> CreateSubscriber(string email)
        {
            try
            {
                var listId = ListId;
                // Use the Status property if updating an existing member
                var member = new Member { EmailAddress = $"githubTestAccount@test.com", StatusIfNew = Status.Subscribed };
                member.MergeFields.Add("FNAME", "HOLY");
                return await _mailChimpManager.Members.AddOrUpdateAsync(listId, member);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
