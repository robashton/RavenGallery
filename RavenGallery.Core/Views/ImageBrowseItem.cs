using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core.Views
{
    public class ImageBrowseItem
    {
        public string Title
        {
            get;
            private set;
        }

        public string Filename
        {
            get;
            private set;
        }

        public ImageBrowseItem(string title, string filename)
        {
            this.Title = title;
            this.Filename = filename;
        }
    }
}
