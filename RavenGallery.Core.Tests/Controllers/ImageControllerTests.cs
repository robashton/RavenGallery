using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RavenGallery.Controllers;
using Moq;
using System.Web;
using RavenGallery.ViewModels;
using RavenGallery.Core.Commands;
using System.IO;
using RavenGallery.Core.Views;
using System.Web.Mvc;

namespace RavenGallery.Core.Tests.Controllers
{
    [TestFixture]
    public class ImageControllerTests
    {
        public Mock<ICommandInvoker> CommandInvokerMock { get; set; }
        public Mock<IViewRepository> ViewRepositoryMock { get; set; }
        public ImageController Controller { get; set; }

        [SetUp]
        public void CreateObjects()
        {
            CommandInvokerMock = new Mock<ICommandInvoker>();
            ViewRepositoryMock = new Mock<IViewRepository>();
            Controller = new ImageController(CommandInvokerMock.Object, ViewRepositoryMock.Object);
        }

        [Test]
        public void WhenNewImageIsSubmittedWithInvalidModelState_CommandIsNotSent()
        {
            Mock<HttpPostedFileBase> postedFileMock = new Mock<HttpPostedFileBase>();
            Controller.ModelState.AddModelError("Something", "Something");

            Controller.New("userId", postedFileMock.Object, new ImageNewViewModel());

            CommandInvokerMock.Verify(x => x.Execute(It.IsAny<UploadUserImageCommand>()), Times.Never());        
        }

        [Test]
        public void WhenNewImageIsSubmittedWithValidModelStateButNoFile_CommandIsNotSent()
        {
            Controller.New("userId", null, new ImageNewViewModel());

            CommandInvokerMock.Verify(x => x.Execute(It.IsAny<UploadUserImageCommand>()), Times.Never());    
        }
        
        [Test]
        public void WhenNewImageIsSubmittedWIthValidModelAndFile_CommandIsSent()
        {
            Mock<HttpPostedFileBase> postedFileMock = new Mock<HttpPostedFileBase>();
            int length = 100;

            postedFileMock.Setup(x => x.InputStream.Read(
                It.Is<byte[]>(array=> array.Length == length),
                0,
                length)).Returns(length);
            

            Controller.New("userId", postedFileMock.Object, new ImageNewViewModel());

            CommandInvokerMock.Verify(x => x.Execute(It.IsAny<UploadUserImageCommand>()), Times.Once()); 
        }

        [Test]
        public void When_GetTagsIsExecutedJsonModelIsReturned()
        {
            var input = new ImageTagCollectionInputModel()
            {
                SearchText = "text"
            };
            var output = new ImageTagCollectionView(new List<ImageTagCollectionItem>());
            ViewRepositoryMock.Setup(x => x.Load<ImageTagCollectionInputModel, ImageTagCollectionView>(input)).Returns(output);

            var result = Controller._GetTags(input) as JsonResult;

            Assert.AreEqual(output, result.Data);
        }

        [Test]
        public void When_GetBrowseDataIsExecutedJsonModelIsReturned()
        {
            var input = new ImageBrowseInputModel(){
               Page = 10,
               PageSize = 100
            };
            var output = new ImageBrowseView(0, 100, new List<ImageBrowseItem>());
            ViewRepositoryMock.Setup(x => x.Load<ImageBrowseInputModel, ImageBrowseView>(input)).Returns(output);

            var result = Controller._GetBrowseData(input) as JsonResult;

            Assert.AreEqual(output, result.Data);            
        }
    }
}
