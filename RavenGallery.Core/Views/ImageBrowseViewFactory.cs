using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
using RavenGallery.Core.Documents;
using RavenGallery.Core.Indexes;

namespace RavenGallery.Core.Views
{
    public class ImageBrowseViewFactory : IViewFactory<ImageBrowseInputModel, ImageBrowseView>
    {
        private IDocumentSession documentSession;

        public ImageBrowseViewFactory(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;
        }

        public ImageBrowseView Load(ImageBrowseInputModel input)
        {
            // Adjust the model appropriately
            input.PageSize = input.PageSize == 0 || input.PageSize > 20 ? 20 : input.PageSize;

            // Perform the paged query
            var query = documentSession.Query<ImageDocument, Images_ByTag>()
                    .Skip(input.Page * input.PageSize)
                    .Take(input.PageSize);

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                query = query.Where(x => x.Tags.Any(y => y.Name == input.SearchText));
            }

            // And enact this query
            var items = query              
                .ToArray()
                .Select(x => new ImageBrowseItem(x.Title, x.Filename));
               
            return new ImageBrowseView(
                input.Page,
                input.PageSize,
                input.SearchText,
                items);
        }
    }
}
