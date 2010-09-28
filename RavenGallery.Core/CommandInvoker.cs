using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using Raven.Client;

namespace RavenGallery.Core
{
    public class CommandInvoker : ICommandInvoker
    {
        private IContainer container;
        private IDocumentSession documentSession;

        public CommandInvoker(IContainer container, IDocumentSession documentSession)
        {
            this.container = container;
            this.documentSession = documentSession;
        }

        public void Execute<T>(T command)
        {
            var handler = container.GetInstance<ICommandHandler<T>>();
            handler.Handle(command);
            documentSession.SaveChanges();
        }
    }
}
