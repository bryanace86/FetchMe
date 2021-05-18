using FetchMe.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Services.ApplicationUserServices
{
    public interface IApplicationUserService
    {
        Task<ApplicationUserDetailsVm> GetUserDetailsVMAsync(string id);
        Task<ApplicationUserDetailsVm> GetUsersDetailsVMAsync();
        Task<ApplicationUserDetailsVm> UpdateUserDetailsVMAsync(ApplicationUserDetailsVm userVm);
        Task<bool> CheckSlugExistAsync(string slug);
        Task<ApplicationUserDetailsVm> GetBySlugAsync(string slug);
        Task<List<ApplicationUserCompleteness>> GetAllUserCompletenessAsync();
    }
}
