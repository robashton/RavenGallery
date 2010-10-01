using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;

namespace RavenGallery.Core
{
    public class ViewRepository : IViewRepository
    {
        private IContainer container;

        public ViewRepository(IContainer container)
        {
            this.container = container;
        }

        public TOutput Load<TInput, TOutput>(TInput input)
        {
            var factory = container.TryGetInstance<IViewFactory<TInput, TOutput>>();
            if (factory == null) return default(TOutput);
            return factory.Load(input);
        }
    }
}
