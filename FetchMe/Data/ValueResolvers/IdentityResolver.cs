using AutoMapper;
using FetchMe.Models.PhotographModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FetchMe.Data.ValueResolvers
{
    public class IdentityResolver : IValueResolver<Photograph, PhotographIndexViewModel, bool>
    {
        
        private readonly HttpContext _context;

        public IdentityResolver(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor.HttpContext;
        }
        
        public bool Resolve(Photograph source, PhotographIndexViewModel destination, bool destMember, ResolutionContext context)
        {
            //return true;
            
            var user = _context.User;
            Guid userId = Guid.Parse(user.FindFirstValue("PhotographerId"));
            bool isOwner = (userId == source.PhotographerId);
            return isOwner;
            
        }
    }
}
