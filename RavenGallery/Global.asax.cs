using System;
using System.Web.Mvc;
using System.Web.Routing;
using RavenGallery.Core;
using StructureMap;
using FluentValidation.Mvc;
using FluentValidation;
using RavenGallery.Core.Web;

namespace RavenGallery
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Image",
                "Resources/Image/{*filename}",
                new { controller = "Resources", action = "Image" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            ModelValidatorProviders.Providers.Add(
                new FluentValidationModelValidatorProvider(new StructureMapValidatorFactory()));
            ControllerBuilder.Current.SetControllerFactory(new RavenGallery.Core.StructureMapControllerFactory());
            ModelBinders.Binders.DefaultBinder = new GenericBinderResolver();
            Bootstrapper.Startup();
        }

        protected void Application_EndRequest()
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }
    }

    public class StructureMapValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            return ObjectFactory.TryGetInstance(validatorType) as IValidator;
        }
    }
}