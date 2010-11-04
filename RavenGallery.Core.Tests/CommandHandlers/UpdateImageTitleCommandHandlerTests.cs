using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RavenGallery.Core.Documents;
using RavenGallery.Core.Entities;
using RavenGallery.Core.Repositories;
using Moq;
using RavenGallery.Core.CommandHandlers;

namespace RavenGallery.Core.Tests.CommandHandlers
{
    [TestFixture]
    public class UpdateImageTitleCommandHandlerTests
    {
        [Test]
        public void WhenCommandIsInvoked_ImageTitleIsUpdated()
        {
            ImageDocument innerDocument = new ImageDocument();
            Image entity = new Image(innerDocument);
            Mock<IImageRepository> imageRepositoryMock = new Mock<IImageRepository>();
            imageRepositoryMock.Setup(x => x.Load("test")).Returns(entity);
            UpdateImageTitleCommandHandler handler = new UpdateImageTitleCommandHandler(imageRepositoryMock.Object);
            handler.Handle(new Commands.UpdateImageTitleCommand("test", "newTitle"));

            Assert.AreEqual("newTitle", innerDocument.Title);
        }
    }
}
