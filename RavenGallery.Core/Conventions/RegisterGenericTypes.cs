using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap.Graph;

namespace RavenGallery.Core.Conventions
{
    public class RegisterGenericTypes : IRegistrationConvention
    {
        private Type baseInterface;

        public RegisterGenericTypes(Type baseInterface)
        {
            this.baseInterface = baseInterface;
        }
        public void Process(Type type, StructureMap.Configuration.DSL.Registry registry)
        {
            if (type.IsAbstract) { return; }
            if (type.IsInterface) { return; }
            var originalInterface = type.GetInterfaces().Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == baseInterface).FirstOrDefault();
            if (originalInterface == null) return;

            Type[] wrappedTypes = originalInterface.GetGenericArguments();

            // Create the created type
            Type implementationType = baseInterface.MakeGenericType(wrappedTypes);

            // And specify what we're going to use
            registry.For(implementationType).Use(type);  

        }
    }
}
