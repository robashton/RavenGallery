using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core.Documents
{
    public class Image
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public List<ImageTag> Tags { get; set; }
    }
}
