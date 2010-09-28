using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RavenGallery.Core.Documents;
using RavenGallery.Core.Services;
using RavenGallery.Core.Utility;

namespace RavenGallery.Core.Tests.Integration.Services
{
    [TestFixture]
    public class UserServiceTests : LocalRavenTest
    {
        [Test]
        public void WhenUserExists_DoesUserExistWithUsernameAndPassword_ReturnsTrue()
        {
            using (var session = Store.OpenSession())
            {
                session.Store(new UserDocument()
                {
                    PasswordHash = HashUtil.HashPassword("password"),
                    Username = "testUser"
                });
                session.SaveChanges();

                UserService service = new UserService(session);
                bool result = service.DoesUserExistWithUsernameAndPassword("testUser", "password");
                Assert.True(result);
            }
        }

        [Test]
        public void WhenUserDoesNotExist_DoesUserExistWithUsernameAndPassword_ReturnsFalse()
        {
            using (var session = Store.OpenSession())
            {
                session.Store(new UserDocument()
                {
                    PasswordHash = HashUtil.HashPassword("password"),
                    Username = "testUser"
                });
                session.SaveChanges();

                UserService service = new UserService(session);
                bool result = service.DoesUserExistWithUsernameAndPassword("testUser", "password2");
                Assert.False(result);
            }
        }

        [Test]
        public void WhenUserExists_DoesUserExistWithUsername_ReturnsTrue()
        {
            using (var session = Store.OpenSession())
            {
                session.Store(new UserDocument()
                {
                     PasswordHash = "pass",
                     Username = "testUser"
                });
                session.SaveChanges();

                UserService service = new UserService(session);
                bool result = service.DoesUserExistWithUsername("testUser");
                Assert.True(result);
            }
        }

        [Test]
        public void WhenUserDoesNotExist_DoesUserExistWithUsername_ReturnsFalse()
        {
            using (var session = Store.OpenSession())
            {
                session.Store(new UserDocument()
                {
                    PasswordHash = "pass",
                    Username = "testUser"
                });
                session.SaveChanges();

                UserService service = new UserService(session);
                bool result = service.DoesUserExistWithUsername("testOtherUser");
                Assert.False(result);
            }
        }

        public string HashUtility { get; set; }
    }
}
