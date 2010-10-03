using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
using RavenGallery.Core.Documents;
using RavenGallery.Core.Indexes;

namespace RavenGallery.Core.Views
{
    public class ImageTagCollectionViewFactory : IViewFactory<ImageTagCollectionInputModel, ImageTagCollectionView>
    {
        private IDocumentSession documentSession;

        public ImageTagCollectionViewFactory(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;
        }

        public ImageTagCollectionView Load(ImageTagCollectionInputModel input)
        {
            var query = this.documentSession.Query<ImageTagCollectionItem, ImageTags_GroupByTagName>()
                .Take(25);

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                query = query.Where(x => x.Name.StartsWith(input.SearchText));
            }

            var results = query.ToArray();
            return new ImageTagCollectionView(results);                
        }
    }
}
