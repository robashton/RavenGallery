using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap.Configuration.DSL;
using Raven.Client;
using RavenGallery.Core.Conventions;
using FluentValidation;

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

            Scan(s =>
            {
                s.AssembliesFromApplicationBaseDirectory();
                s.With(new RegisterGenericTypes(typeof(IViewFactory<,>)));
                s.With(new RegisterGenericTypes(typeof(ICommandHandler<>)));
                s.With(new RegisterGenericTypes(typeof(IValidator<>)));
                s.WithDefaultConventions();
            });
        }
    }
}
