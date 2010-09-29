using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap.Graph;
using System.Reflection;

namespace RavenGallery.Core.Conventions
{
    public class RegisterFirstInstanceOfInterface : IRegistrationConvention
    {
        public void Process(Type type, StructureMap.Configuration.DSL.Registry registry)
        {
            if (!type.IsInterface) { return; }
            if (type.IsGenericTypeDefinition) { return; }

            Assembly containingAssembly = type.Assembly;
            var matchedType = containingAssembly.GetTypes()
                                    .Where(x => 
                                            x.Namespace == type.Namespace
                                        &&  x.GetInterface(type.FullName) != null)
                                    .FirstOrDefault();
            if (matchedType == null) { return; }

            registry.For(type).Use(matchedType);           
        }
    }
}
