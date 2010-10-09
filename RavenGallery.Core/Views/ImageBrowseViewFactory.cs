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
            var query = documentSession.Query<ImageDocument>()
                    .Skip(input.Page * input.PageSize)
                    .Take(input.PageSize);

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                query = query.Where(x => x.Title.StartsWith(input.SearchText) || x.Tags.Any(y => y.Name.StartsWith(input.SearchText)));
            }

            // And enact this query
            // Arse, 
            var items = query            
                .Select(x=> new ImageBrowseItem(x.Title, x.Filename))
                .ToArray();
               
            return new ImageBrowseView(
                input.Page,
                input.PageSize,
                input.SearchText,
                items.Select(x=> new ImageBrowseItem(x.Title, x.Filename)));
        }
    }
}
