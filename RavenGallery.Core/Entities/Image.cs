using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RavenGallery.Core.Documents;

namespace RavenGallery.Core.Entities
{
    public class Image : IEntity<ImageDocument>
    {
        private ImageDocument innerDocument;

        public Image(string ownerUserId, string title)
        {
            innerDocument = new ImageDocument()
            {
                DateUploaded = DateTime.Now,
                OwnerUserId = ownerUserId,
                Title = title
            };
        }

        public Image(ImageDocument innerDocument){
            this.innerDocument = innerDocument;
        }

        ImageDocument IEntity<ImageDocument>.GetInnerDocument()
        {
            return this.innerDocument;
        }
    }
}
