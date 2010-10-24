using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RavenGallery.Core.Utility;
using RavenGallery.Core.Documents;

namespace RavenGallery.Core.Entities
{
    public class User : IEntity<UserDocument>
    {
        private UserDocument innerDocument;

        public string UserId { get { return innerDocument.Id; } }

        public User(string username, string password)
        {
            innerDocument = new UserDocument()
            {
                Id = IdUtil.CreateUserId(username),
                PasswordHash = HashUtil.HashPassword(password),
                Username = username
            };
        }

        public User(UserDocument innerUser)
        {
            this.innerDocument = innerUser;
        }

        UserDocument IEntity<UserDocument>.GetInnerDocument()
        {
            return innerDocument;
        }
    }
}
