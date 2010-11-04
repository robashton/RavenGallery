using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core.Views
{
    public class ImageBrowseItem
    {
        public string Id
        {
            get;
            private set;
        }
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

        public ImageBrowseItem(string id, string title, string filename)
        {
            this.Id = id;
            this.Title = title;
            this.Filename = filename;
        }
    }
}
