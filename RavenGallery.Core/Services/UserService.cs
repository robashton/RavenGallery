using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
using RavenGallery.Core.Documents;
using RavenGallery.Core.Utility;

namespace RavenGallery.Core.Services
{
    public class UserService : IUserService
    {
        public IDocumentSession documentSession;

        public UserService(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;
        }

        public bool DoesUserExistWithUsername(string username)
        {
            return documentSession.DynamicQuery<UserDocument>()
                .Where(x => x.Username == username)
                .Any();
        }

        public bool DoesUserExistWithUsernameAndPassword(string username, string password)
        {
            String hashedPass = HashUtil.HashPassword(password);
            return documentSession.DynamicQuery<UserDocument>()
                .Where(x => x.Username == username && x.PasswordHash == hashedPass)
                .Any();
        }
    }
}
