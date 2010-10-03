using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core.Views
{
    public class ImageTagCollectionView
    {
        public IEnumerable<ImageTagCollectionItem> Items { get; private set; }

        public ImageTagCollectionView(IEnumerable<ImageTagCollectionItem> items)
        {
            this.Items = items;
        }
    }
}
