using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap.Configuration.DSL;
using Raven.Client;
using RavenGallery.Core.Conventions;
using FluentValidation;
using RavenGallery.Core.Web;

namespace RavenGallery.Core
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry(IDocumentStore documentStore)
        {
            Scan(s =>
            {
                s.AssembliesFromApplicationBaseDirectory(x => x.FullName.StartsWith("RavenGallery"));
                s.With(new RegisterGenericTypesOfInterface(typeof(IViewFactory<,>)));
                s.With(new RegisterGenericTypesOfInterface(typeof(ICommandHandler<>)));
                s.With(new RegisterGenericTypesOfInterface(typeof(IValidator<>)));
                s.With(new RegisterGenericTypesOfInterface(typeof(IModelBinder<>)));
                s.With(new RegisterFirstInstanceOfInterface());
            });

            For<IDocumentStore>().Use(documentStore);
            For<IDocumentSession>()
                .HybridHttpOrThreadLocalScoped()
                .Use(x =>
                {
                    var store = x.GetInstance<IDocumentStore>();
                    return store.OpenSession();
                });
        }
    }
}
