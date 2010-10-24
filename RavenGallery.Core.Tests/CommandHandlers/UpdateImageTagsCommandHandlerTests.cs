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
    public class UpdateImageTagsCommandHandlerTests
    {
        [Test]
        public void WhenCommandIsInvoked_ImageIsUpdated()
        {
            ImageDocument innerDocument = new ImageDocument();
            Image entity = new Image(innerDocument);
            Mock<IImageRepository> imageRepositoryMock = new Mock<IImageRepository>();
            imageRepositoryMock.Setup(x => x.Load("test")).Returns(entity);
            UpdateImageTagsCommandHandler handler = new UpdateImageTagsCommandHandler(imageRepositoryMock.Object);
            var newTags = new[] { "one", "two" };
            handler.Handle(new Commands.UpdateImageTagsCommand("test", newTags));

            Assert.True(
                innerDocument.Tags[0].Name == "one" &&
                innerDocument.Tags[1].Name == "two");
        }
    }
}
