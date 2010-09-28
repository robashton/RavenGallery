using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RavenGallery.Controllers;
using Moq;
using RavenGallery.Core.Commands;
using RavenGallery.Core.Security;

namespace RavenGallery.Core.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        Mock<ICommandInvoker> commandInvoker = null;
        Mock<IAuthenticationService> authenticationService = null; 
        UserController userController = null;

        [SetUp]
        public void CreateObjects()
        {
             commandInvoker = new Mock<ICommandInvoker>();
             authenticationService = new Mock<IAuthenticationService>();
             userController = new UserController(commandInvoker.Object, authenticationService.Object);
        }

        [Test]
        public void WhenRegisterPostedWithInvalidModel_CommandIsNotSent()
        {
            userController.ModelState.AddModelError("Something", "Whatever");
            userController.Register(new ViewModels.UserRegisterViewModel() { Password = "pass", Username = "user" });
            commandInvoker.Verify(x => x.Execute(It.IsAny<RegisterNewUserCommand>()), Times.Never());
        }

        [Test]
        public void WhenRegisterPostedWithInvalidModel_UserIsNotLoggedIn()
        {
            userController.ModelState.AddModelError("Something", "Whatever");
            userController.Register(new ViewModels.UserRegisterViewModel() { Password = "pass", Username = "user" });
            authenticationService.Verify(x => x.SignIn(It.IsAny<string>(), It.IsAny<bool>()), Times.Never());
        }

        [Test]
        public void WhenRegisterPostedWithValidModel_UserIsLoggedIn()
        {
            userController.Register(new ViewModels.UserRegisterViewModel() { Password = "pass", Username = "user" });
            authenticationService.Verify(x => x.SignIn("user", false), Times.Once());
        }

        [Test]
        public void WhenRegisterPostedWithValidModel_CommandIsSent()
        {
            userController.Register(new ViewModels.UserRegisterViewModel() { Password = "pass", Username = "user" });
            commandInvoker.Verify(x => x.Execute(It.Is<RegisterNewUserCommand>(y => y.Password == "pass" && y.Username == "user")), Times.Once());
        }

        [Test]
        public void WhenSignInCalledWithValidModel_UserIsLoggedIn()
        {
            userController.SignIn(new ViewModels.UserSignInViewModel() { Password = "pass", Username = "user" });
            authenticationService.Verify(x => x.SignIn("user", false), Times.Once());
        }

        [Test]
        public void WhenSignInCalledWithInvalidModel_UserIsNotLoggedIn()
        {
            userController.ModelState.AddModelError("Something", "Whatever");
            userController.SignIn(new ViewModels.UserSignInViewModel() { Password = "pass", Username = "user" });
            authenticationService.Verify(x => x.SignIn(It.IsAny<string>(), It.IsAny<bool>()), Times.Never());
        }
    }
}
