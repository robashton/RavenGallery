using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
using RavenGallery.Core.Documents;

namespace RavenGallery.Core.Views
{
    public class ImageViewFactory : IViewFactory<ImageViewInputModel, ImageView>
    {
        private IDocumentSession documentSession;

        public ImageViewFactory(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;
        }
        public ImageView Load(ImageViewInputModel input)
        {
            var doc = documentSession.Load<ImageDocument>(input.ImageId);
            return new ImageView(
                doc.Filename,
                doc.Tags.Select(tag => tag.Name).ToArray(),
                doc.Title);
        }
    }
}
