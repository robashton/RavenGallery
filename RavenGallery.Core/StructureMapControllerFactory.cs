using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using StructureMap;
using System.Web.Routing;

namespace RavenGallery.Core
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) { return null; }
            return ObjectFactory.GetInstance(controllerType) as Controller;
        }
    }
}
