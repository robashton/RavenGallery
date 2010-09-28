using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using Raven.Client;
using RavenGallery.Core.Documents;
using RavenGallery.Core.Services;
using RavenGallery.Validators;

namespace RavenGallery.Core.Tests.Validators
{
    [TestFixture]
    public class UserRegisterViewModelValidatorTests
    {
        [Test]
        public void WhenUserExistsValidationFails()
        {
            Mock<IUserService> sessionMock = new Mock<IUserService>();
            sessionMock.Setup(x => x.DoesUserExistWithUsername("test")).Returns(true);
            UserRegisterViewModelValidator validator = new UserRegisterViewModelValidator(sessionMock.Object);
            var results = validator.Validate(new ViewModels.UserRegisterViewModel()
            {
                 Username = "test",
                 Password = "test",
                 StayLoggedIn = true
            });

            Assert.False(results.IsValid);
        }

        [Test]
        public void WhenUserDoesNotExistValidationSucceeds()
        {
            Mock<IUserService> sessionMock = new Mock<IUserService>();
            sessionMock.Setup(x => x.DoesUserExistWithUsername("test")).Returns(false);
            UserRegisterViewModelValidator validator = new UserRegisterViewModelValidator(sessionMock.Object);
            var results = validator.Validate(new ViewModels.UserRegisterViewModel()
            {
                Username = "test",
                Password = "test",
                StayLoggedIn = true
            });

            Assert.True(results.IsValid);
        }

    }
}
