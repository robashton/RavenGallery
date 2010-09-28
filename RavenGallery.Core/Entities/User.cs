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
        private UserDocument innerUser;        

        public User(string username, string password)
        {
            innerUser = new UserDocument()
            {
                 PasswordHash = HashUtil.HashPassword(password),
                 Username = username
            };
        }

        public User(UserDocument innerUser)
        {
            this.innerUser = innerUser;
        }

        UserDocument IEntity<UserDocument>.GetInnerDocument()
        {
            return innerUser;
        }
    }
}
