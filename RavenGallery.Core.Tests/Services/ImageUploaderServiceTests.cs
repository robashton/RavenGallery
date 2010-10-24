using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RavenGallery.Core.Infrastructure;
using Moq;
using RavenGallery.Core.Repositories;
using RavenGallery.Core.Services;
using RavenGallery.Core.Entities;

namespace RavenGallery.Core.Tests.Services
{
    public class ImageUploaderServiceTests
    {
        public Mock<IFileStorageService> FileStorageServiceMock;
        public Mock<IImageRepository> ImageRepositoryMock;
        public ImageUploaderService ImageUploaderService;

        [SetUp]
        public void CreateObjects()
        {
            FileStorageServiceMock = new Mock<IFileStorageService>();
            ImageRepositoryMock = new Mock<IImageRepository>();
            ImageUploaderService = new ImageUploaderService(FileStorageServiceMock.Object, ImageRepositoryMock.Object);
        }
    }

    [TestFixture]
    public class WhenImageIsUploaded : ImageUploaderServiceTests
    {
        [SetUp]
        public void EnactTest()
        {
            ImageUploaderService.UploadUserImage(
                new Core.Entities.User("test", "password"), 
                "title", 
                new string[] {"tagOne"},
                new byte[] { 3,1,4,1,5,9 });
        }

        [Test]
        public void ImageIsPersistedToRepository()
        {
            ImageRepositoryMock.Verify(x=>x.Add(It.IsAny<Image>()), Times.Once());
        }

        [Test]
        public void FileIsSentToStorage()
        {
            FileStorageServiceMock.Verify(x => x.StoreFile(It.IsAny<string>(), It.IsAny<Byte[]>()), Times.Once());
        }
    }
}
