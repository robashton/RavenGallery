using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RavenGallery.Core.Entities;

namespace RavenGallery.Core
{
    public interface IEntityRepository<TEntity, TDocument> where TEntity : IEntity<TDocument>
    {
        TEntity Load(string id);
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
