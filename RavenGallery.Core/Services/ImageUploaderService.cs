using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RavenGallery.Core.Entities;
using RavenGallery.Core.Infrastructure;
using RavenGallery.Core.Repositories;

namespace RavenGallery.Core.Services
{
    public class ImageUploaderService : IImageUploaderService
    {
        private IFileStorageService fileStorageService;
        private IImageRepository imageRepository;

        public ImageUploaderService(IFileStorageService fileStorageService, IImageRepository imageRepository)
        {
            this.fileStorageService = fileStorageService;
            this.imageRepository = imageRepository;
        }

        public void UploadUserImage(User user, string title, string[] tags, byte[] data)
        {
            string filename = String.Format("Images/{0}", Guid.NewGuid().ToString());
            fileStorageService.StoreFile(filename, data);

            Image newImage = new Image(user, filename, title);
            foreach (var tag in tags)
            {
                newImage.AddTag(tag);
            }
            imageRepository.Add(newImage);
        }
    }
}
