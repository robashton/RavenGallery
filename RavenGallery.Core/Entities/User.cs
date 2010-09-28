using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docs = RavenGallery.Core.Documents;
using RavenGallery.Core.Utility;

namespace RavenGallery.Core.Entities
{
    public class User : IEntity<Docs.UserDocument>
    {
        private Docs.UserDocument innerUser;        

        public User(string username, string password)
        {
            innerUser = new Docs.UserDocument()
            {
                 PasswordHash = HashUtil.HashPassword(password),
                 Username = username
            };
        }

        public User(Docs.UserDocument innerUser)
        {
            this.innerUser = innerUser;
        }

        Docs.UserDocument IEntity<Docs.UserDocument>.GetInnerDocument()
        {
            return innerUser;
        }
    }
}
