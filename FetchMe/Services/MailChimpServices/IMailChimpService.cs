using MailChimp.Net.Models;
using System.Threading.Tasks;

namespace FetchMe.Services.MailChimpServices
{
    public interface IMailChimpService
    {
        Task<Member> UpsertSubscriberAsync();
        Task<bool> IsUserSubscribedAsync();
        Task<Member> UnsubscribeUserAsync();
    }
}
