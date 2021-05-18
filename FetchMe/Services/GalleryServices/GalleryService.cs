using AutoMapper;
using FetchMe.Data;
using FetchMe.Data.Migrations;
using FetchMe.Models;
using FetchMe.Models.GalleryModels;
using FetchMe.Models.PhotographModels;
using FetchMe.Models.TagModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace FetchMe.Services.GalleryServices
{
    public class GalleryService : IGalleryService
    {
        private readonly ILogger<GalleryService> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<IdentityUser> _roleManager;

        public GalleryService(
            ILogger<GalleryService> logger,
            ApplicationDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager
            //RoleManager<IdentityUser> roleManager
            )
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            //_roleManager = roleManager;
        }

        public async Task<UpsertImageInGalleryVM> Create(GalleryCreateVM galleryCreateVM)
        {
            try
            {
                string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Gallery gallery = new Gallery()
                {
                    OwnerID = userId,
                    Title = galleryCreateVM.Title,
                    IsPublic = galleryCreateVM.IsPublic
                };

                if (galleryCreateVM.IsProfessional && _httpContextAccessor.HttpContext.User.IsInRole("Photographer"))
                {
                    gallery.PhotographerId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue("PhotographerId"));
                }

                _context.Galleries.Add(gallery);

                if (galleryCreateVM.ImageId != null)
                {
                    _context.GalleryImages.Add(new GalleryImage()
                    {
                        Gallery = gallery,
                        PhotographId = Guid.Parse(galleryCreateVM.ImageId.ToString())
                    });
                }

                await _context.SaveChangesAsync();

                UpsertImageInGalleryVM upsertImageInGalleryVM = _mapper.Map<UpsertImageInGalleryVM>(gallery);

                if (galleryCreateVM.ImageId != null)
                {
                    upsertImageInGalleryVM.Thumbnail = _context.Photographs.FirstOrDefault(x => x.Id == Guid.Parse(galleryCreateVM.ImageId.ToString())).ImageUrl;
                }
                return upsertImageInGalleryVM;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<UpsertImageInGalleryGrouped> GetUsersGalleriesForImageAsync(Guid imageId)
        {
            try
            {
                string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                string photographerId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "PhotographerId")?.Value;

                Photograph photograph = await _context.Photographs.FirstOrDefaultAsync(x => x.Id == imageId);

                IEnumerable<Gallery> galleries = _context.Galleries
                    .Include(x => x.GalleryImages)
                    .ThenInclude(x => x.Photograph)
                    .Where(x => x.OwnerID == userId);

                List<UpsertImageInGalleryVM> upsertImageInGalleryVM = new List<UpsertImageInGalleryVM>();
                List<UpsertImageInGalleryVM> upsertImageNotGalleryVM = new List<UpsertImageInGalleryVM>();

                foreach (Gallery gallery in galleries)
                {
                    UpsertImageInGalleryVM galleryTemp = new UpsertImageInGalleryVM()
                    {
                        Id = gallery.Id,
                        Title = gallery.Title,
                        IsProfessional = (gallery.PhotographerId != null),
                        IsPublic = gallery.IsPublic,
                        IsInGallery = gallery.GalleryImages.Any(y => y.PhotographId == imageId),
                        IsOwnerOfImage = (photograph.PhotographerId.ToString() == photographerId),
                        Thumbnail = (gallery.GalleryImages.Any())? gallery.GalleryImages.First()?.Photograph?.ImageUrl: null
                    };

                    bool displayGallery = true;
                    if (gallery.PhotographerId != null)
                    {
                        if (gallery.PhotographerId != Guid.Parse(photographerId) || photograph.PhotographerId != Guid.Parse(photographerId))
                        {
                            displayGallery = false;
                        }
                    }

                    if (displayGallery)
                    {
                        if (galleryTemp.IsInGallery)
                        {
                            upsertImageInGalleryVM.Add(galleryTemp);
                        }
                        else
                        {
                            upsertImageNotGalleryVM.Add(galleryTemp);
                        }
                    }

                }

                UpsertImageInGalleryGrouped upsertImageInGalleryGrouped = new UpsertImageInGalleryGrouped()
                {
                    InGallery = upsertImageInGalleryVM,
                    NotInGallery = upsertImageNotGalleryVM
                };

                return upsertImageInGalleryGrouped;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> AddImageToGallery(GalleryImageVM galleryImage)
        {
            //check if user can add image to the gallery
            Gallery gallery = await _context.Galleries.FirstOrDefaultAsync(x => x.Id == galleryImage.GalleryId);
            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (gallery.OwnerID != userId) return false;

            GalleryImage galleryImageDto = _mapper.Map<GalleryImage>(galleryImage);

            _context.GalleryImages.Add(galleryImageDto);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveImageFromGallery(GalleryImageVM galleryImage)
        {
            //check if user can add image to the gallery
            Gallery gallery = await _context.Galleries.FirstOrDefaultAsync(x => x.Id == galleryImage.GalleryId);
            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (gallery.OwnerID != userId) return false;

            GalleryImage galleryImageDto = await _context.GalleryImages.FirstOrDefaultAsync(x => x.GalleryId == galleryImage.GalleryId && x.PhotographId == galleryImage.PhotographId);

            _context.GalleryImages.Remove(galleryImageDto);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ImageGalleryCreateOptionsVM> GetGalleryOptionsForImageAsync(Guid imageId)
        {
            Photograph photograph = await _context.Photographs.FirstOrDefaultAsync(x => x.Id == imageId);

            bool isOwner = false;
            if (_httpContextAccessor.HttpContext.User.IsInRole("Photographer"))
            {
                isOwner = (photograph.PhotographerId == Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue("PhotographerId")));
            }

            ImageGalleryCreateOptionsVM createOptions = new ImageGalleryCreateOptionsVM()
            {
                ImageId = imageId,
                IsOwnerOfImage = isOwner
            };

            return createOptions;
        }

        public async Task<GalleryIndexVM> GetGalleries(GallerySearch search)
        {
            IQueryable<Gallery> galleriesQ = _context.Galleries
                .Include(x => x.GalleryImages)
                .ThenInclude(x => x.Photograph);

            if(search.PhotographerId != null)
            {
                galleriesQ = galleriesQ.Where(x => 
                    x.PhotographerId == search.PhotographerId);
            }

            if(search.OwnerId != null)
            {
                galleriesQ = galleriesQ.Where(x => 
                    x.OwnerID == search.OwnerId 
                    );

            }

            galleriesQ = galleriesQ.Where(x =>
                    x.IsPublic 
                    || _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) == x.OwnerID
                    );

            IEnumerable <Gallery> galleries = await galleriesQ.ToListAsync();

            IEnumerable<GalleryIndexItemVM> galleryIndexItemVM = _mapper.Map<IEnumerable<GalleryIndexItemVM>>(galleries);
            GalleryIndexVM galleryIndexVM = new GalleryIndexVM()
            {
                Galleries = galleryIndexItemVM,
                Search = search
            };

            return galleryIndexVM;
        }
    }
}
