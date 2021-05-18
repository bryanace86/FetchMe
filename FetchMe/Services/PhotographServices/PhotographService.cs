using AutoMapper;
using FetchMe.Data;
using FetchMe.Models.LocationModels;
using Geolocation;
using FetchMe.Models.PhotographerModels;
using FetchMe.Models.PhotographModels;
using FetchMe.Models.PhotographTagModels;
using FetchMe.Services.BlobServices;
using FetchMe.Services.LocationServices;
using FetchMe.Services.PhotographerServices;
using FetchMe.Services.TagServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AutoMapper.QueryableExtensions;
using Z.EntityFramework.Plus;
using Microsoft.AspNetCore.Identity;
using FetchMe.Models;

namespace FetchMe.Services.PhotographServices
{
    public class PhotographService : IPhotographService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBlobService _blobService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotographerService _photographerService;
        private readonly ILocationService _locationService;
        private readonly UserManager<ApplicationUser> _userManager;


        public PhotographService(
            ApplicationDbContext context,
            IBlobService blobService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IPhotographerService photographerService,
            UserManager<ApplicationUser> userManager,
            ILocationService locationService
            )
        {
            _context = context;
            _blobService = blobService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _photographerService = photographerService;
            _locationService = locationService;
            _userManager = userManager;
        }

        [Authorize(Policy = "HasPhotographesRemaining")]
        public async Task<PhotographDto> CreateAsync(PhotographCreateDto input)
        {
            try
            {
                PhotographerDto photographerDto = await _photographerService.GetByUser();
                input.PhotographerId = photographerDto.Id;

                input.ImageUrl = CreateImageUrl(CreateUniqueFileName(input.ImageTitle));

                byte[] data = Convert.FromBase64String(input.ImageData);
                PhotographDimensions photographDimensions = await _blobService.UploadImage(input.ImageUrl, data);

                if (photographDimensions.Dimensions.Count() == 0) throw new Exception();

                if (input.Location.FormattedAddress == null)
                {
                    input.Location = null;
                }

                Photograph photograph = _mapper.Map<Photograph>(input);

                photograph = SetImageTag(photograph);
                photograph.PhotographTags = SetImageTags(input.PhotographTagIds);

                if (photograph.Location != null)
                {
                    photograph.Location = await _locationService.GetUpdatedLocation(_mapper.Map<Location>(photograph.Location));
                    photograph.LocationId = photograph.Location.Id;
                }

                SetImageDeminsions(photograph, photographDimensions);

                photograph.Created = DateTime.Now;
                //photograph.Created = GetRandomDateTime();

                _context.Photographs.Add(photograph);
                _context.SaveChanges();

                if (input.IsBannerImage) SetBannerImage(photograph, input.PhotographerId);
                if (input.IsLogoImage) SetLogoImage(photograph, input.PhotographerId);
                _context.SaveChanges();

                PhotographDto photographDto = _mapper.Map<PhotographDto>(photograph);

                return photographDto;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        
        private void SetImageDeminsions(Photograph photograph, PhotographDimensions photographDimensions)
        {
            photograph.Width = photographDimensions.Dimensions.FirstOrDefault(x => x.Size == 100).Width;
            photograph.Height = photographDimensions.Dimensions.FirstOrDefault(x => x.Size == 100).Height;

            photograph.SWidth = photographDimensions.Dimensions.FirstOrDefault(x => x.Size == 300).Width;
            photograph.SHeight = photographDimensions.Dimensions.FirstOrDefault(x => x.Size == 300).Height;

            photograph.MWidth = photographDimensions.Dimensions.FirstOrDefault(x => x.Size == 600).Width;
            photograph.MHeight = photographDimensions.Dimensions.FirstOrDefault(x => x.Size == 600).Height;

            photograph.LWidth = photographDimensions.Dimensions.FirstOrDefault(x => x.Size == 1500).Width;
            photograph.LHeight = photographDimensions.Dimensions.FirstOrDefault(x => x.Size == 1500).Height;
        }

        public string CreateUniqueFileName(string imageTitle)
        {
            //imageTitle = imageTitle.TrimStart().TrimEnd().Replace(" ", "-");
            //string newTitle = $"{imageTitle}-{Guid.NewGuid()}";
            string newTitle = $"{Guid.NewGuid()}";
            return newTitle;
        }

        public string CreateImageUrl(string imageTitle)
        {
            return $"{imageTitle}.jpeg";
        }

        public async Task<bool> DeleteById(Guid id)
        {
            PhotographerDto photographerDto = await _photographerService.GetByUser();
            PhotographDto photograph = await GetByIdAsync(id);
            if (photograph.PhotographerId != photographerDto.Id) throw new Exception();

            _context.Photographs.Remove(await _context.Photographs.FirstOrDefaultAsync(x => x.Id == id));

            bool imagesAreDeleted = await _blobService.DeleteImages(photograph.ImageUrl);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<PhotographIndexViewModel>> GetAllAsync(PhotographSearch search)
        {
            var user = _httpContextAccessor.HttpContext.User;
            string userId = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            Guid photographerId = Guid.Parse(user.Claims.FirstOrDefault(c => c.Type == "PhotographerId")?.Value ?? Guid.Empty.ToString());

            IQueryable<Photograph> photographQuery = _context.Photographs
            .Include(x => x.Photographer)
            .Include(x => x.Location)
            .Include(x => x.PhotographTags)
            .IncludeFilter(x => x.PhotographLikes.Where(y => y.UserId == userId));

            if (search.GalleryId != null)
            {
                photographQuery = photographQuery.Include(x => x.Galleries)
                    .Where(x => x.Galleries.Any(y => y.GalleryId == search.GalleryId));
            }
            
            
            if (search.CurrentFirstItemCreated != null && search.CurrentLastItemCreated != null)
            {
                photographQuery = photographQuery.Where(x =>
                    (
                        (x.Created >= search.CurrentFirstItemCreated && x.Id != search.CurrentFirstItem) ||
                        (x.Created <= search.CurrentLastItemCreated && x.Id != search.CurrentLastItem)
                    ));
            }

            if (search.PhotographerSlug != null)
            {
                photographQuery = photographQuery.Where(
                    x => x.Photographer.Slug == search.PhotographerSlug
                    );
            }

            if (search.PhotographTagIds != null)
            {
                photographQuery = photographQuery.Where(
                    x => x.PhotographTags.Any(
                        y => search.PhotographTagIds.Contains(y.PhotographTagId)
                        )
                    );
            }

            if (search.CameraBody != null)
            {
                photographQuery = photographQuery.Where(
                    x => search.CameraBody.Contains(x.CameraBodyValue)
                    );
            }

            if (search.ExposureTime != null)
            {
                photographQuery = photographQuery.Where(
                     x => search.ExposureTime.Contains(x.ExposureTimeValue)
                     );
            }

            if (search.FStop != null)
            {
                photographQuery = photographQuery.Where(
                    x => search.FStop.Contains(x.FStopValue)
                    );
            }

            if (search.ISO != null)
            {
                photographQuery = photographQuery.Where(
                    x => search.ISO.Contains(x.ISOValue)
                    );
            }

            if (search.LightSource != null)
            {
                photographQuery = photographQuery.Where(
                    x => search.LightSource.Contains(x.LightSourceValue)
                    );
            }

            if (search.Lens != null)
            {
                photographQuery = photographQuery.Where(
                    x => search.Lens.Contains(x.LensValue)
                    );
            }

            if (search.FocalLength != null)
            {
                photographQuery = photographQuery.Where(
                    x => search.FocalLength.Contains(x.FocalLengthValue)
                    );
            }

            if (search.Location != null)
            {
                if (search.Location.Lat != 0 && search.Location.Lng != 0)
                {
                    var CenterLat = Convert.ToDouble(search.Location.Lat);
                    var Centerlong = Convert.ToDouble(search.Location.Lng);
                    var distance = search.Distance;
                    CoordinateBoundaries boundaries = new CoordinateBoundaries(CenterLat, Centerlong, distance);

                    Decimal minLatitude = Convert.ToDecimal(boundaries.MinLatitude);
                    Decimal maxLatitude = Convert.ToDecimal(boundaries.MaxLatitude);
                    Decimal minLongitude = Convert.ToDecimal(boundaries.MinLongitude);
                    Decimal maxLongitude = Convert.ToDecimal(boundaries.MaxLongitude);

                    photographQuery = photographQuery
                        .Where(x =>
                            x.Location.Lat >= minLatitude && x.Location.Lat <= maxLatitude &&
                            x.Location.Lng >= minLongitude && x.Location.Lng <= maxLongitude
                            );

                }
            }
            
            if (search.ShowFavorites)
            {
                photographQuery = photographQuery.Where(
                    x => x.PhotographLikes.Any(y => y.PhotographId == x.Id && y.UserId == userId)
                    );
            }

            int defaultPageSize = (search.PageSize != 0) ? search.PageSize : 25;

            photographQuery = photographQuery
                .Where(x => !x.HideFromGallery || x.PhotographerId == photographerId)
                .OrderByDescending(x => x.Created);

            List<Photograph> photographsDto = await photographQuery
                    .Take(defaultPageSize)
                    .ToListAsync();

            List<PhotographIndexViewModel> photographs = _mapper.Map<List<PhotographIndexViewModel>>(photographsDto);

            if (photographerId != Guid.Empty)
            {
                Guid id = photographerId;
                foreach (PhotographIndexViewModel p in photographs)
                {
                    p.IsOwner = p.PhotographerId == id;
                }
            }
            

            return photographs;
            
        }

        public async Task<PhotographDto> GetByIdAsync(Guid id)
        {
            Photograph photograph = await _context.Photographs
                .Include(p => p.CameraBody)
                .Include(p => p.ExposureTime)
                .Include(p => p.FStop)
                .Include(p => p.FocalLength)
                .Include(p => p.ISO)
                .Include(p => p.Lens)
                .Include(p => p.LightSource)
                .Include(p => p.Location)
                .Include(p => p.PhotographTags)
                .Include(p => p.Photographer)
                .FirstOrDefaultAsync(x => x.Id == id);
            PhotographDto photographDto = _mapper.Map<PhotographDto>(photograph);
            return photographDto;
        }

        public async Task<PhotographDto> UpdateAsync(PhotographUpdateDto photograph)
        {
            try
            {

                Photograph photographDTO = await _context.Photographs
                        .Include(x => x.Photographer)
                        .Include(x => x.PhotographTags)
                        .Include(x => x.Location)
                        .FirstOrDefaultAsync(x => x.Id == photograph.Id);

                photograph.PhotographerId = photographDTO.PhotographerId;

                photograph.ExposureTime = _context.ExposureTimes.Pick(x => x.Id, photograph.ExposureTimeValue);
                photograph.FStop = _context.FStops.Pick(x => x.Id, photograph.FStopValue);
                photograph.ISO = _context.ISOs.Pick(x => x.Id, photograph.ISOValue);
                photograph.LightSource = _context.LightSources.Pick(x => x.Id, photograph.LightSourceValue);
                photograph.Lens = _context.Lenses.Pick(x => x.Id, photograph.LensValue);
                photograph.FocalLength = _context.FocalLengths.Pick(x => x.Id, photograph.FocalLengthValue);
                photograph.CameraBody = _context.CameraBodies.Pick(x => x.Id, photograph.CameraBodyValue);

                if (photograph.PhotographTagIds != null)
                {
                    List<PhotographTagsPhotograph> photoTags = new List<PhotographTagsPhotograph>();
                    foreach (var tag in photograph.PhotographTagIds)
                    {

                        photoTags.Add(new PhotographTagsPhotograph()
                        {
                            Photograph = photographDTO,
                            PhotographId = photograph.Id,
                            PhotographTag = _context.PhotographTags.Pick(x => x.Id, tag),
                            PhotographTagId = tag
                        });

                    }
                    photograph.PhotographTags = photoTags;
                }

                if (photograph.Location?.Id != null)
                {
                    photograph.Location = await _locationService.GetUpdatedLocation(_mapper.Map<Location>(photograph.Location));
                    photograph.LocationId = photograph.Location.Id;
                }

                photograph.ImageUrl = photographDTO.ImageUrl;

                photographDTO = _mapper.Map<PhotographUpdateDto, Photograph>(photograph, photographDTO);

                if (photographDTO.Location?.Id == null)
                {
                    photographDTO.Location = null;
                }

                if (photograph.IsBannerImage) SetBannerImage(photographDTO, photograph.PhotographerId);
                if (photograph.IsLogoImage) SetLogoImage(photographDTO, photograph.PhotographerId);

                _context.Update(photographDTO);
                await _context.SaveChangesAsync();

                PhotographDto photographDto = _mapper.Map<PhotographDto>(photograph);
                return photographDto;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> GetPhotographCount(){
            PhotographerDto photographerDto = await _photographerService.GetByUser();
            Guid photographerId = photographerDto.Id;
            return _context.Photographs.Where(x => x.PhotographerId == photographerId).Count();
        }

        public async Task<PhotographDto> GetById(Guid id)
        {
            Photograph photograph = await _context.Photographs.FirstOrDefaultAsync(x => x.Id == id);
            PhotographDto photographDto = _mapper.Map<PhotographDto>(photograph);
            return photographDto;
        }

        private Photograph SetImageTag(Photograph photograph)
        {
            photograph.ExposureTime = _context.ExposureTimes.Pick(x => x.Id, photograph.ExposureTimeValue);
            photograph.FStop = _context.FStops.Pick(x => x.Id, photograph.FStopValue);
            photograph.ISO = _context.ISOs.Pick(x => x.Id, photograph.ISOValue);
            photograph.LightSource = _context.LightSources.Pick(x => x.Id, photograph.LightSourceValue);
            photograph.Lens = _context.Lenses.Pick(x => x.Id, photograph.LensValue);
            photograph.FocalLength = _context.FocalLengths.Pick(x => x.Id, photograph.FocalLengthValue);
            photograph.CameraBody = _context.CameraBodies.Pick(x => x.Id, photograph.CameraBodyValue);

            return photograph;
        }

        private List<PhotographTagsPhotograph> SetImageTags(List<string> tags)
        {
            List<PhotographTagsPhotograph> photoTags = new List<PhotographTagsPhotograph>();
            if (tags != null)
            {   
                foreach (var tag in tags)
                {
                    photoTags.Add(new PhotographTagsPhotograph()
                    {
                        PhotographTag = _context.PhotographTags.Pick(x => x.Id, tag),
                        PhotographTagId = tag
                    });

                }
                return photoTags;
            }
            return photoTags;
        }
    
        private void SetBannerImage(Photograph image, Guid photographerId)
        {
            try
            {
                Photographer photographer = _context.Photographers.FirstOrDefault(x => x.Id == photographerId);
                photographer.BannerImage = image;
                _context.Update(photographer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        private void SetLogoImage(Photograph image, Guid photographerId)
        {
            try
            {
                Photographer photographer = _context.Photographers.FirstOrDefault(x => x.Id == photographerId);
                photographer.LogoImage = image;
                _context.Update(photographer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PhotographUpdateDto> MapToUpdateDto(PhotographDto photographDto)
        {
            if (!await IsOwnedByPhotographer(photographDto)) return null;

            PhotographUpdateDto photographUpdateDto = _mapper.Map<PhotographUpdateDto>(photographDto);

            photographUpdateDto.IsBannerImage = IsBannerImage(photographDto);
            photographUpdateDto.IsLogoImage = IsLogoImage(photographDto);

            return photographUpdateDto;

        }

        public bool IsBannerImage(PhotographDto photographDto)
        {
            return (photographDto.Photographer.BannerImageId == photographDto.Id);
        }

        public bool IsLogoImage(PhotographDto photographDto)
        {
            return (photographDto.Photographer.LogoImageId == photographDto.Id);
        }

        public async Task<bool> IsOwnedByPhotographer(PhotographDto photographDto)
        {
            PhotographerDto photographer = await _photographerService.GetByUser();
            return (photographDto.PhotographerId == photographer.Id);

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Photograph photograph = await _context.Photographs.FirstOrDefaultAsync(x => x.Id == id);
            PhotographDto photographDto = _mapper.Map<PhotographDto>(photograph);
            if (!await IsOwnedByPhotographer(photographDto)) return false;
            _context.Photographs.Remove(photograph);
            _context.SaveChanges();

            await _blobService.DeleteImages(photograph.ImageUrl);

            return true;


        }
    
        private DateTime GetRandomDateTime()
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddDays(-100);
            Random r = new Random();
            //for better randomness don't recreate a new Random() too frequently.
            long rand62bit = (((long)r.Next()) << 31) + r.Next();
            // 62bits suffices for random datetimes, 31 does not!
            DateTime newDate = startDate + new TimeSpan(rand62bit % (endDate - startDate).Ticks);
            return newDate;
        }

    }
}
