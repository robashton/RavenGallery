using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RavenGallery.Core.Entities;

namespace RavenGallery.Core.Services
{
    public interface IImageUploaderService
    {
        void UploadUserImage(User user, string title, string[] tags, Byte[] data);
    }
}
