using FetchMe.Models.GalleryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Services.GalleryServices
{
    public interface IGalleryService
    {
        Task<GalleryIndexVM> GetGalleries(GallerySearch search);
        Task<UpsertImageInGalleryVM> Create(GalleryCreateVM gallery);
        Task<bool> Delete(Guid id);
        Task<UpsertImageInGalleryGrouped> GetUsersGalleriesForImageAsync(Guid imageId);
        Task<ImageGalleryCreateOptionsVM> GetGalleryOptionsForImageAsync(Guid imageId);
        Task<bool> AddImageToGallery(GalleryImageVM galleryImage);
        Task<bool> RemoveImageFromGallery(GalleryImageVM galleryImage);
    }
}
