using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
using RavenGallery.Core.Documents;
using System.Diagnostics;

namespace RavenGallery.Core.Views
{
    public class ImageByRelatedImageViewFactory : IViewFactory<ImageByRelatedImageInputModel, ImageByRelatedImageView>
    {
        private IDocumentSession documentSession;

        public ImageByRelatedImageViewFactory(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;
        }

        public ImageByRelatedImageView Load(ImageByRelatedImageInputModel input)
        {
            var tags = documentSession.Load<ImageDocument>(input.ImageId)
                  .Tags.Select(x=>x.Name)
                  .ToArray();
          
          var query = documentSession.Advanced.LuceneQuery<ImageDocument>();
          string orQuery = String.Join(" OR ", tags);

          query = query.Where(string.Format("Tags,Name:({0})", orQuery));
          var results = query.ToArray().Select(x => new ImageByRelatedViewItem()
          {
              Filename = x.Filename,
              Id = x.Id
          }).ToArray();

          return new ImageByRelatedImageView()
          {
               Items = results
          };
        }
    }
}
