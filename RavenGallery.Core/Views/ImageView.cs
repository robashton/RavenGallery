using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core.Views
{
    public class ImageView
    {
        public string Filename { get; set; }
        public string[] Tags { get; set; }
        public string Title { get; set; }

        public ImageView(string filename, string[] tags, string title)
        {
            this.Filename = filename;
            this.Tags = tags;
            this.Title = title;
        }
    }
}
