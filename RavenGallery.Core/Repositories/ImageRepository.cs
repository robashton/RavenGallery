using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RavenGallery.Core.Entities;
using RavenGallery.Core.Documents;
using Raven.Client;

namespace RavenGallery.Core.Repositories
{
    public class ImageRepository : EntityRepository<Image, ImageDocument>, IImageRepository
    {
        public ImageRepository(IDocumentSession documentSession) : base(documentSession) { }

        protected override Image Create(ImageDocument doc)
        {
            return new Image(doc);
        }
    }
}
