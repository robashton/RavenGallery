using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap.Configuration.DSL;
using Raven.Client;

namespace RavenGallery.Core
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry(IDocumentStore documentStore)
        {
            For<IDocumentStore>().Use(documentStore);
            For<IDocumentSession>()
                .HttpContextScoped()
                .Use(x =>
                {
                    var store = x.GetInstance<IDocumentStore>();
                    return store.OpenSession();
                });
        }
    }
}
