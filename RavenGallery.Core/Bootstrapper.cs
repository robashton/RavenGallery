using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using Raven.Client.Document;
using Raven.Database;

namespace RavenGallery.Core
{
    public static class Bootstrapper
    {
        public static void Startup()
        {
            var documentStore = new DocumentStore
            {
                Configuration = new RavenConfiguration
                {
                    DataDirectory = "App_Data\\RavenDB",
                }
            };
            documentStore.Initialize();

            ObjectFactory.Initialize(config =>
            {
                config.AddRegistry(new CoreRegistry(documentStore));
            });
        }
    }
}
