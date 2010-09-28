using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
using RavenGallery.Core.Documents;

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
    }
}
