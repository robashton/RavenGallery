using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core.Documents
{
    public class ImageDocument
    {
        public string Id
        {
            get;
            set;
        }

        public string OwnerUserId
        { 
            get; 
            set; 
        }

        public DateTime DateUploaded
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Filename
        {
            get;
            set;
        }
        
        public List<ImageTagDocument> Tags
        {
            get;
            set;
        }

        public ImageDocument() { Tags = new List<ImageTagDocument>(); }
    }
}
