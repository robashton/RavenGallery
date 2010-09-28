using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RavenGallery.Core.CommandHandlers;
using RavenGallery.Core.Repositories;
using Moq;
using RavenGallery.Core.Entities;

namespace RavenGallery.Core.Tests.CommandHandlers
{
    [TestFixture]
    public class RegisterNewUserCommandHandlerTests
    {
        [Test]
        public void WhenCommandIsReceived_NewUserIsAddedToRepository()
        {
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            RegisterNewUserCommandHandler handler = new RegisterNewUserCommandHandler(userRepositoryMock.Object);
            handler.Handle(new Commands.RegisterNewUserCommand("user", "pass"));
            userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once());
        }
    }
}
