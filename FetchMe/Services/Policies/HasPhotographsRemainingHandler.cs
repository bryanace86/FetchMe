using FetchMe.Services.PhotographServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FetchMe.Services.Policies
{
    public class HasPhotographsRemainingHandler : AuthorizationHandler<HasPhotographsRemainingRequirement>
    {

        private IPhotographService PhotographServices { get; set; }
        
        public HasPhotographsRemainingHandler(
            IPhotographService photographServices
            )
        {
            PhotographServices = photographServices;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            HasPhotographsRemainingRequirement requirement)
        {
            if (!await HasPhotographsRemaining(context, requirement))
            {
                if (context.Resource is AuthorizationFilterContext redirectContext)
                    redirectContext.Result = new RedirectResult("/Photographs/Index");
            }
            context.Succeed(requirement);
        }

        protected async Task<bool> HasPhotographsRemaining(
            AuthorizationHandlerContext context,
            HasPhotographsRemainingRequirement requirement
            )
        {

            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Name))
            {
                return false;
            }
            int photographersImageCount = await PhotographServices.GetPhotographCount();

            if (photographersImageCount < requirement.MaxImages)
            {
                return true;
            }

            return false;

        }

    }
}
