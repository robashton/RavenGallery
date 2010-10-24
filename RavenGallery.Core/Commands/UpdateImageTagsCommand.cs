using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core.Commands
{
    public class UpdateImageTagsCommand
    {
        public string ImageId { get; set; }
        public string[] Tags { get; set; }

        public UpdateImageTagsCommand(string imageId, string[] tags)
        {
            this.ImageId = imageId;
            this.Tags = tags;
        }
    }
}
