using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RavenGallery.Controllers;
using RavenGallery.Core.Infrastructure;
using Moq;
using System.Web.Mvc;

namespace RavenGallery.Core.Tests.Controllers
{
    [TestFixture]
    public class ResourcesControllerTests
    {
        [Test]
        public void WhenImageInvoked_FileIsReturned()
        {
            Mock<IFileStorageService> fileStorageServiceMock = new Mock<IFileStorageService>();
            ResourcesController resourcesController = new ResourcesController(fileStorageServiceMock.Object);

            Byte[] fileBytes = new Byte[]{};
            fileStorageServiceMock.Setup(x => x.RetrieveFile("test")).Returns(fileBytes);

            var result = resourcesController.Image("test") as FileContentResult;
            Assert.AreEqual(fileBytes, result.FileContents);
        }
    }
}
