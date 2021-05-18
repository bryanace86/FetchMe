using Azure.Storage.Blobs.Models;
using FetchMe.Models.PhotographModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Services.BlobServices
{
    public interface IBlobService
    {
        Task<PhotographDimensions> UploadImage(string fileName, byte[] data);
        Task<PhotographDimension> ResizeBase64Async(string fileName, byte[] data, int size, int quality);
        Task<bool> DeleteImages(string fileName);
    }
}
