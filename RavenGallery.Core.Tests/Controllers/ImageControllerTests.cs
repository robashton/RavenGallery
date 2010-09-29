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

namespace RavenGallery.Core.Tests.Controllers
{
    [TestFixture]
    public class ImageControllerTests
    {
        [Test]
        public void WhenNewImageIsSubmittedWithInvalidModelState_CommandIsNotSent()
        {
            Mock<ICommandInvoker> commandInvokerMock = new Mock<ICommandInvoker>();
            Mock<HttpPostedFileBase> postedFileMock = new Mock<HttpPostedFileBase>();
            ImageController controller = new ImageController(commandInvokerMock.Object);
            controller.ModelState.AddModelError("Something", "Something");

            controller.New(postedFileMock.Object, new ImageNewViewModel());

            commandInvokerMock.Verify(x => x.Execute(It.IsAny<UploadUserImageCommand>()), Times.Never());        
        }

        [Test]
        public void WhenNewImageIsSubmittedWithValidModelStateButNoFile_CommandIsNotSent()
        {
            Mock<ICommandInvoker> commandInvokerMock = new Mock<ICommandInvoker>();
            ImageController controller = new ImageController(commandInvokerMock.Object);

            controller.New(null, new ImageNewViewModel());

            commandInvokerMock.Verify(x => x.Execute(It.IsAny<UploadUserImageCommand>()), Times.Never());    
        }
        
        [Test]
        public void WhenNewImageIsSubmittedWIthValidModelAndFile_CommandIsSent()
        {
            Mock<ICommandInvoker> commandInvokerMock = new Mock<ICommandInvoker>();
            Mock<HttpPostedFileBase> postedFileMock = new Mock<HttpPostedFileBase>();
            int length = 100;

            postedFileMock.Setup(x => x.InputStream.Read(
                It.Is<byte[]>(array=> array.Length == length),
                0,
                length)).Returns(length);

            ImageController controller = new ImageController(commandInvokerMock.Object);

            controller.New(postedFileMock.Object, new ImageNewViewModel());

            commandInvokerMock.Verify(x => x.Execute(It.IsAny<UploadUserImageCommand>()), Times.Once()); 
        }
    }
}
