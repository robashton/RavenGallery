using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
using RavenGallery.Core.Documents;
using RavenGallery.Core.Utility;
using RavenGallery.Core.Indexes;

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
            return documentSession.Query<UserDocument,Users_ByUsername>()
                .Where(x => x.Username == username.ToLowerInvariant())
                .Any();
        }

        public bool DoesUserExistWithUsernameAndPassword(string username, string password)
        {
            String hashedPass = HashUtil.HashPassword(password);
            return documentSession.Query<UserDocument, Users_ByUsernameAndPassword>()
                .Where(x => x.Username == username.ToLowerInvariant() && x.PasswordHash == hashedPass)
                .Any();
        }
    }
}
