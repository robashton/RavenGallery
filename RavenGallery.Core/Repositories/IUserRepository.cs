using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RavenGallery.Core.Entities;
using RavenGallery.Core.Documents;

namespace RavenGallery.Core.Repositories
{
    public interface IUserRepository : IEntityRepository<User, UserDocument>
    {

    }
}
