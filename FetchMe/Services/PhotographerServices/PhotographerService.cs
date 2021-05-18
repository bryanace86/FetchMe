using AutoMapper;
using FetchMe.Data;
using FetchMe.Data.Migrations;
using FetchMe.Models;
using FetchMe.Models.LocationModels;
using FetchMe.Models.PhotographerModels;
using FetchMe.Models.PhotographModels;
using FetchMe.Services.MailChimpServices;
using Geolocation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace FetchMe.Services.PhotographerServices
{
    public class PhotographerService : IPhotographerService
    {
        private readonly ILogger<PhotographerService> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMailChimpService _mailChimp;

        public PhotographerService(
            ILogger<PhotographerService> logger,
            ApplicationDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMailChimpService mailChimp
            )
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
            _mailChimp = mailChimp;
            //_roleManager = roleManager;
        }

        public async Task<List<PhotographerIndexViewModel>> GetAllAsync(PhotographerSearch search)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            IQueryable<Photographer> photographersQuery = _context.Photographers
                .IncludeFilter(
                    x => x.Photographs.Where(
                        x =>
                            search.PhotographTagIds == null ||
                            (
                                x.PhotographTags.Any(z => search.PhotographTagIds.Contains(z.PhotographTagId))
                                && x.HideFromGallery.Equals(false)
                            )

                        )
                        .Take(25)
                    )
                .IncludeFilter(x => x.PhotographerLikes.Where(y => y.UserId == userId))
                .Where(x =>
                    x.Photographs.Any() &&
                    (
                        search.PhotographTagIds == null ||
                        x.Photographs.Any(y =>
                            y.PhotographTags.Any(z => search.PhotographTagIds.Contains(z.PhotographTagId))
                            && y.HideFromGallery.Equals(false)
                            )
                    )

                )
                .OrderByDescending(x => x.Created);
                //.ToList();

            if (search.PhotographerName != null)
            {
                photographersQuery = photographersQuery.Where(
                    x =>
                    search.PhotographerName.Contains(x.DisplayName)
                    //x.DisplayName == search.PhotographerName
                    );
            }

            if (search.Location?.Lat != null)
            {
                if (search.Location.Lat != (decimal)0.00)
                {
                    var CenterLat = Convert.ToDouble(search.Location.Lat);
                    var Centerlong = Convert.ToDouble(search.Location.Lng);
                    var distance = search.Distance;
                    CoordinateBoundaries boundaries = new CoordinateBoundaries(CenterLat, Centerlong, distance);

                    Decimal minLatitude = Convert.ToDecimal(boundaries.MinLatitude);
                    Decimal maxLatitude = Convert.ToDecimal(boundaries.MaxLatitude);
                    Decimal minLongitude = Convert.ToDecimal(boundaries.MinLongitude);
                    Decimal maxLongitude = Convert.ToDecimal(boundaries.MaxLongitude);

                    List<Location> serviceableLocations = _context.Locations.Where(x =>
                            x.Lat >= minLatitude && x.Lat <= maxLatitude &&
                            x.Lng >= minLongitude && x.Lng <= maxLongitude
                        ).ToList();

                    photographersQuery = photographersQuery.Where(
                        x => x.Locations.Any(
                            y => serviceableLocations.Contains(y.Location)
                            )
                        );
                }

            }

            if (search.IsInsured)
            {
                photographersQuery = photographersQuery.Where(
                    x => x.IsInsured);
            }

            if (search.ShowFavorites)
            {
                //var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                photographersQuery = photographersQuery.Where(
                    x => x.PhotographerLikes.Any(y => y.PhotographerId == x.Id && y.UserId == userId)
                    );
            }

            List<Photographer> photographers = await photographersQuery
                   .Skip(search.PageIndex * search.PageSize)
                   //.Take(2).ToListAsync();
                   .Take((search.PageSize == 0) ? 10 : search.PageSize).ToListAsync();

            List<PhotographerIndexViewModel> photographerVM = _mapper.Map<List<PhotographerIndexViewModel>>(photographers);

            return photographerVM;

        }
        
        public async Task<PhotographerDto> CreateAsync(PhotographerCreateDto input)
        {
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                string userId = user.Id;

                if (UserHasPhotographer()) throw new Exception();

                input.OwnerID = userId;
                Photographer photographer = _mapper.Map<Photographer>(input);

                photographer.Created = DateTime.Now;

                await _context.Photographers.AddAsync(photographer);
                var result = await _userManager.AddToRoleAsync(user, "Photographer");

                await _userManager.AddClaimAsync(user, new Claim("PhotographerId", photographer.Id.ToString(), "Guid", "FetchMe"));
                await _userManager.AddClaimAsync(user, new Claim("PhotographerSlug", photographer.Slug.ToString(), "string", "FetchMe"));

                await _context.SaveChangesAsync();

                await _signInManager.RefreshSignInAsync(user);

                PhotographerDto photographerDto = _mapper.Map<PhotographerDto>(photographer);

                if (input.SubscribeToNewsletter)
                {
                    await _mailChimp.UpsertSubscriberAsync();
                }

                return photographerDto;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        
        public async Task<PhotographerDto> UpdateAsync(PhotographerUpdateDto input)
        {
            ApplicationUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            string userId = user.Id;

            Photographer photographer = await _context.Photographers.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (photographer.OwnerID != userId) throw new Exception();

            photographer = _mapper.Map(input, photographer);
            
            _context.Photographers.Update(photographer);
            await _userManager.RemoveClaimAsync(user, _userManager.GetClaimsAsync(user).Result.FirstOrDefault(x => x.Type == "PhotographerSlug"));
            await _userManager.AddClaimAsync(user, new Claim("PhotographerSlug", photographer.Slug.ToString(), "string", "FetchMe"));

            await _context.SaveChangesAsync();

            await _signInManager.RefreshSignInAsync(user);
            
            PhotographerDto photographerDto = _mapper.Map<PhotographerDto>(photographer);

            var isSubscribed = await _mailChimp.IsUserSubscribedAsync();

            if (input.SubscribeToNewsletter && !isSubscribed)
            {
                await _mailChimp.UpsertSubscriberAsync();
            }
            else if (!input.SubscribeToNewsletter && isSubscribed)
            {
                await _mailChimp.UnsubscribeUserAsync();
            }

            return photographerDto;
        }

        public async Task<PhotographerDto> GetByUser()
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Photographer photographer = await _context.Photographers.FirstOrDefaultAsync(x => x.OwnerID == userId);
            PhotographerDto photographerDto = _mapper.Map<PhotographerDto>(photographer);
            return photographerDto;
        }
        
        public PhotographerDto GetBySlug(string slug)
        {
            Photographer photographer = _context.Photographers.FirstOrDefault(x => x.Slug == slug);
            PhotographerDto photographerDto = _mapper.Map<PhotographerDto>(photographer);
            return photographerDto;
        }

        public bool CheckSlugExist(string slug)
        {
            PhotographerDto photographerDto = GetBySlug(slug);
            return photographerDto != null ? true : false;
        }

        public bool UserHasPhotographer()
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _context.Photographers.Any(x => x.OwnerID == userId);
        }

        public async Task<PhotographerUpdateDto> MapToUpdateDtoAsync(PhotographerDto photographerDto)
        {
            PhotographerUpdateDto photographerUpdateDto = _mapper.Map<PhotographerUpdateDto>(photographerDto);

            photographerUpdateDto.SubscribeToNewsletter = await _mailChimp.IsUserSubscribedAsync();
            return photographerUpdateDto;
        }

        public PhotographerDetailsDto GetPhotographerDetails(string slug)
        {
            Photographer photographer = _context.Photographers
                .Include(x => x.BannerImage)
                .Include(x => x.LogoImage)
                .IncludeFilter(x => x.PhotographerLikes
                    .Where(x => x.UserId == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)))
                .FirstOrDefault(x => x.Slug == slug);
            PhotographerDetailsDto photographerDto = _mapper.Map<PhotographerDetailsDto>(photographer);
            photographerDto.IsLiked = photographer.PhotographerLikes.Any();
            photographerDto.IsOwner = IsOwner(photographerDto.OwnerID);
            return photographerDto;
        }

        public bool IsOwner(string ownerId)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return (ownerId == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            
            return false;
        }
    }
}
