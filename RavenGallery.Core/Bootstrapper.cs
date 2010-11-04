using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using Raven.Client.Document;
using Raven.Database;
using Raven.Client.Indexes;
using RavenGallery.Core.Indexes;
using Raven.Client.Client;

namespace RavenGallery.Core
{
    public static class Bootstrapper
    {
        public static void Startup()
        {
            var documentStore = new EmbeddableDocumentStore
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


            IndexCreation.CreateIndexes(typeof(ImageTags_GroupByTagName).Assembly, documentStore);
        }
    }
}
