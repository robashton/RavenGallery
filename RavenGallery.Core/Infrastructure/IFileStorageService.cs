using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core.Infrastructure
{
    public interface IFileStorageService
    {
        void StoreFile(string filename, Byte[] bytes);
    }
}
