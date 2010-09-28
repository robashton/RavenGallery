using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RavenGallery.Controllers;
using Moq;
using RavenGallery.Core.Commands;

namespace RavenGallery.Core.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        [Test]
        public void WhenRegisterPostedWithInvalidModel_CommandIsNotSent()
        {
            Mock<ICommandInvoker> commandInvoker = new Mock<ICommandInvoker>();
            UserController userController = new UserController(commandInvoker.Object);
            userController.ModelState.AddModelError("Something", "Whatever");
            userController.Register(new ViewModels.UserRegisterViewModel() { Password = "pass", Username = "user" });
            commandInvoker.Verify(x => x.Execute(It.IsAny<RegisterNewUserCommand>()), Times.Never());
        }

        [Test]
        public void WhenRegisterPostedWithValidModel_CommandIsSent()
        {
            Mock<ICommandInvoker> commandInvoker = new Mock<ICommandInvoker>();
            UserController userController = new UserController(commandInvoker.Object);
            userController.Register(new ViewModels.UserRegisterViewModel() { Password = "pass", Username = "user" });
            commandInvoker.Verify(x => x.Execute(It.Is<RegisterNewUserCommand>(y => y.Password == "pass" && y.Username == "user")), Times.Once());
        }
    }
}
