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

        public string ImageId { get { return innerDocument.Id; } }
        
        public Image(User owner, string title, string filename)
        {
            innerDocument = new ImageDocument()
            {
                DateUploaded = DateTime.Now,
                OwnerUserId = owner.UserId,
                Title = title,
                Filename = filename
            };
        }

        public Image(ImageDocument innerDocument){
            this.innerDocument = innerDocument;
        }

        public void AddTag(string tag)
        {
            innerDocument.Tags.Add(new ImageTagDocument() { Name = tag });
        }

        ImageDocument IEntity<ImageDocument>.GetInnerDocument()
        {
            return this.innerDocument;
        }
    }
}
