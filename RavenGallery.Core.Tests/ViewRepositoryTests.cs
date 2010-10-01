using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using StructureMap;

namespace RavenGallery.Core.Tests
{
    [TestFixture]
    public class ViewRepositoryTests
    {
        [Test]
        public void WhenLoadInvokedAndFactoryIsRegistered_ViewIsReturned()
        {
            Mock<IContainer> containerMock = new Mock<IContainer>();
            Mock<IViewFactory<string, string>> viewFactoryMock = new Mock<IViewFactory<string, string>>();
            containerMock.Setup(x => x.TryGetInstance<IViewFactory<string, string>>()).Returns(viewFactoryMock.Object);
            viewFactoryMock.Setup(x => x.Load("hello")).Returns("hi");
            ViewRepository repository = new ViewRepository(containerMock.Object);
            String result = repository.Load<string, string>("hello");
            Assert.AreEqual("hi", result);
        }

        [Test]
        public void WhenLoadInvokedAndNoFactoryIsRegistered_NullIsReturned()
        {
            Mock<IContainer> containerMock = new Mock<IContainer>();
            ViewRepository repository = new ViewRepository(containerMock.Object);
            String result = repository.Load<string, string>("hello");
            Assert.AreEqual(null, result);
        }
    }
}
