using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core.Commands
{
    public class UploadUserImageCommand
    {
        public string OwnerUserId
        {
            get;
            private set;
        }

        public string Title
        {
            get;
            private set;
        }

        public string[] Tags
        {
            get;
            private set;
        }

        public byte[] Data
        {
            get;
            private set;
        }

        public UploadUserImageCommand(string ownerUserId, string title, string[] tags, Byte[] data)
        {
            this.OwnerUserId = ownerUserId;
            this.Title = title;
            this.Tags = tags;
            this.Data = data;
        }
    }
}
