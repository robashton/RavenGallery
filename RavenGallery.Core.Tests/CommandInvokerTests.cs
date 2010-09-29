using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using StructureMap;
using Moq;
using Raven.Client;

namespace RavenGallery.Core.Tests
{
    [TestFixture]
    public class CommandInvokerTests
    {
        [Test]
        public void WhenCommandIsSentAndHandlerIsPresent_HandlerIsInvoked()
        {
            ObjectFactory.ResetDefaults();
            Mock<ICommandHandler<String>> mockHandler = new Mock<ICommandHandler<string>>();
            Mock<IDocumentSession> documentSessionMock = new Mock<IDocumentSession>();
            ObjectFactory.Configure(x => x.For<ICommandHandler<string>>().Use(mockHandler.Object));

            CommandInvoker invoker = new CommandInvoker(ObjectFactory.Container, documentSessionMock.Object);
            invoker.Execute("SomeString");
            mockHandler.Verify(x => x.Handle("SomeString"), Times.Once());
        }

        [Test]
        public void WhenCommandIsSentAndHandlerIsPresent_SessionIsFlushed()
        {
            ObjectFactory.ResetDefaults();
            Mock<ICommandHandler<String>> mockHandler = new Mock<ICommandHandler<string>>();
            Mock<IDocumentSession> documentSessionMock = new Mock<IDocumentSession>();
            ObjectFactory.Configure(x => x.For<ICommandHandler<string>>().Use(mockHandler.Object));

            CommandInvoker invoker = new CommandInvoker(ObjectFactory.Container, documentSessionMock.Object);
            invoker.Execute("SomeString");
            documentSessionMock.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Test]
        public void WhenCommandIsSentAndHandlerIsPresentButExceptionIsThrown_SessionIsNotFlushed()
        {
            ObjectFactory.ResetDefaults();
            Mock<ICommandHandler<String>> mockHandler = new Mock<ICommandHandler<string>>();
            mockHandler.Setup(x => x.Handle(It.IsAny<string>())).Throws<Exception>();
            Mock<IDocumentSession> documentSessionMock = new Mock<IDocumentSession>();
            ObjectFactory.Configure(x => x.For<ICommandHandler<string>>().Use(mockHandler.Object));

            CommandInvoker invoker = new CommandInvoker(ObjectFactory.Container, documentSessionMock.Object);
            Assert.Throws<Exception>(() => invoker.Execute("SomeString"));
            documentSessionMock.Verify(x => x.SaveChanges(), Times.Never());
        }

        [Test]
        public void WhenCommandIsSentAndHandlerIsNotPresent_ExceptionThrown()
        {
            ObjectFactory.ResetDefaults();
            Mock<IDocumentSession> documentSessionMock = new Mock<IDocumentSession>();
            CommandInvoker invoker = new CommandInvoker(ObjectFactory.Container, documentSessionMock.Object);
            Assert.Throws<StructureMapException>(() => invoker.Execute("SomeString"));
        }

        [Test]
        public void WhenCommandIsSentAndHandlerIsNotPresent_SessionIsNotFlushed()
        {
            ObjectFactory.ResetDefaults();
            Mock<IDocumentSession> documentSessionMock = new Mock<IDocumentSession>();
            CommandInvoker invoker = new CommandInvoker(ObjectFactory.Container, documentSessionMock.Object);

            try { invoker.Execute("SomeString"); }
            catch { }

            documentSessionMock.Verify(x => x.SaveChanges(), Times.Never());
        }
    }
}
