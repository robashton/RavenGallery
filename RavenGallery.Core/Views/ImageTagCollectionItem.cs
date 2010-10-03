using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core.Views
{
    public class ImageTagCollectionItem
    {
        public string Name { get; private set; }
        public int Count { get; set; }

        public ImageTagCollectionItem(string name, int count)
        {
            this.Name = name;
            this.Count = count;
        }
    }
}
