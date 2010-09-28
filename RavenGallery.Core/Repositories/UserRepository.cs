using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RavenGallery.Core.Entities;
using RavenGallery.Core.Documents;
using Raven.Client;

namespace RavenGallery.Core.Repositories
{
    public class UserRepository : EntityRepository<User, UserDocument>, IUserRepository
    {
        public UserRepository(IDocumentSession documentSession) : base(documentSession) { }

        protected override User Create(UserDocument doc)
        {
            return new User(doc);
        }
    }
}
