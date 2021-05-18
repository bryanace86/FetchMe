using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FetchMe.Models.PhotographModels;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Services.BlobServices
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(
            BlobServiceClient blobServiceClient
            )
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<PhotographDimensions> UploadImage(string fileName, byte[] data)
        {

            PhotographDimension a = await ResizeBase64Async(fileName, data, 1500, 90);
            PhotographDimension b = await ResizeBase64Async(fileName, data, 600, 90);
            PhotographDimension c = await ResizeBase64Async(fileName, data, 300, 90);
            PhotographDimension d = await ResizeBase64Async(fileName, data, 100, 90);

            PhotographDimensions imageDemintions = new PhotographDimensions();
            imageDemintions.Dimensions.Add(a);
            imageDemintions.Dimensions.Add(b);
            imageDemintions.Dimensions.Add(c);
            imageDemintions.Dimensions.Add(d);

            return imageDemintions;
        }

        public async Task<PhotographDimension> ResizeBase64Async(string fileName, byte[] data, int size, int quality)
        {
            
            SKBitmap srcBitmap = SKBitmap.Decode(data);

            int width = srcBitmap.Width,
                height = srcBitmap.Height,
                maxWidth = 1920,
                maxHeight = 1080;

            PhotographDimension photographDimension = new PhotographDimension()
            {
                Size = size,
                Width = srcBitmap.Width,
                Height = srcBitmap.Height
            };

            //Max Height 350px Width 350px - for horizontal scroll on all devices
            if (size == 100)
            {
                photographDimension = ResizeHeight(225, photographDimension);
            }
            else if (size == 300)
            {
                photographDimension = ResizeWidth(350, photographDimension);
            }
            //Max Height 800px Max Width 1200px - full screen large devices (> 400 px wide)
            else if (size == 600)
            {
                photographDimension = ResizeHeight(650, photographDimension);

            }
            //Max Height 1080px Max Width 1920px - full screen large devices (> 400 px wide)
            else if (size == 1500)
            {
                photographDimension = ResizeHeight(1080, photographDimension);
            }

            //var blockBlob = container.GetBlockBlobReference(inputPath);
            var blockBlob = _blobServiceClient.GetBlobContainerClient(size.ToString());

            //quality = (width > height) ? 75 : 90;

            //SKImageInfo resizeInfo = new SKImageInfo(srcBitmap.Width, srcBitmap.Height);
            SKImageInfo resizeInfo = new SKImageInfo(photographDimension.Width, photographDimension.Height);
            using (SKBitmap resizedSKBitmap = srcBitmap.Resize(resizeInfo, SKFilterQuality.High))
            using (SKImage newImg = SKImage.FromPixels(resizedSKBitmap.PeekPixels()))
            using (SKData imageData = newImg.Encode(SKEncodedImageFormat.Jpeg, quality))
            using (Stream imgStream = imageData.AsStream())
            {
                //await blockBlob.UploadFromStreamAsync(imgStream);
                await blockBlob.UploadBlobAsync(fileName, imgStream);

            }

            return photographDimension;
        }

        private PhotographDimension Resize(int newWidth, int newHeight, PhotographDimension photographDimension)
        {
            int width = photographDimension.Width;
            int height = photographDimension.Height;

            if (width > height) //Landscape
            {
                if (width > newWidth)
                {
                    width = newWidth;
                    height = height * newWidth / width;
                }
                else if (height > newHeight)
                {
                    width = width * newHeight / width;
                    height = newHeight;
                }
            }
            else //Portrait
            {
                if (width > newWidth)
                {
                    width = width * newHeight / height;
                    height = newHeight;
                }
                else if (height > newHeight)
                {
                    width = newWidth;
                    height = height * newWidth / width;
                }
            }

            photographDimension.Width = width;
            photographDimension.Height = height;

            return photographDimension;
        }

        private PhotographDimension ResizeHeight(int newHeight, PhotographDimension photographDimension)
        {
            float ratio = (float)newHeight / photographDimension.Height;
            photographDimension.Height = newHeight;
            photographDimension.Width = (int)(photographDimension.Width * ratio);

            return photographDimension;
        }

        private PhotographDimension ResizeWidth(int newWidth, PhotographDimension photographDimension)
        {
            float ratio = (float)newWidth / photographDimension.Width;
            photographDimension.Width = newWidth;
            photographDimension.Height = (int)(photographDimension.Height * ratio);

            return photographDimension;
        }

        public async Task<bool> DeleteImages(string fileName)
        {
            bool a = await DeleteImage(fileName, "1500");
            bool b = await DeleteImage(fileName, "600");
            bool c = await DeleteImage(fileName, "300");
            return a || b || c;
        }

        private async Task<bool> DeleteImage(string fileName, string container)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(container);
                var blobClient = containerClient.GetBlobClient(fileName);
                await blobClient.DeleteIfExistsAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
