using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core.Commands
{
    public class UpdateImageTitleCommand
    {
        public string Title { get; set; }
        public string ImageId { get; set; }

        public UpdateImageTitleCommand(string imageId, string title)
        {
            this.Title = title;
            this.ImageId = imageId;
        }
    }
}
