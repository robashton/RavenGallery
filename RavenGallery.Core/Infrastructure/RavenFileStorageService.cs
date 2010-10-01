using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;

namespace RavenGallery.Core.Infrastructure
{
    public class RavenFileStorageService : IFileStorageService
    {
        private IDocumentStore documentStore;

        public RavenFileStorageService(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }
        public void StoreFile(string filename, byte[] bytes)
        {
            documentStore.DatabaseCommands.PutAttachment(filename, null, bytes, new Newtonsoft.Json.Linq.JObject());
        }

        public byte[] RetrieveFile(string filename)
        {
            var file = documentStore.DatabaseCommands.GetAttachment(filename);
            return file != null ? file.Data : null;
        }
    }
}
