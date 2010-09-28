using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RavenGallery.Core.Services;
using Moq;
using RavenGallery.Validators;

namespace RavenGallery.Core.Tests.Validators
{
    [TestFixture]
    public class UserLogonViewModelValidatorTests
    {
        [Test]
        public void WhenUserExistsValidationSucceeds()
        {
            Mock<IUserService> sessionMock = new Mock<IUserService>();
            sessionMock.Setup(x => x.DoesUserExistWithUsernameAndPassword("test", "pass")).Returns(true);
            UserSignInViewModelValidator validator = new UserSignInViewModelValidator(sessionMock.Object);
            var results = validator.Validate(new ViewModels.UserSignInViewModel()
            {
                Username = "test",
                Password = "pass",
                StayLoggedIn = true
            });

            Assert.True(results.IsValid);
        }

        [Test]
        public void WhenUserDoesNotExistValidationFails()
        {
            Mock<IUserService> sessionMock = new Mock<IUserService>();
            sessionMock.Setup(x => x.DoesUserExistWithUsernameAndPassword("test", "pass")).Returns(false);
            UserSignInViewModelValidator validator = new UserSignInViewModelValidator(sessionMock.Object);
            var results = validator.Validate(new ViewModels.UserSignInViewModel()
            {
                Username = "test",
                Password = "pass",
                StayLoggedIn = true
            });

            Assert.False(results.IsValid);
        }
    }
}
