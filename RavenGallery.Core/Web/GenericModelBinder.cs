using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using StructureMap;

namespace RavenGallery.Core.Web
{
    public class GenericBinderResolver : DefaultModelBinder
    {
        private static readonly Type BinderType = typeof(IModelBinder<>);

        public override object BindModel(ControllerContext controllerContext,
                                         ModelBindingContext bindingContext)
        {
            Type genericBinderType = BinderType.MakeGenericType(bindingContext.ModelType);

            var binder = ObjectFactory.TryGetInstance(genericBinderType) as IModelBinder;
            if (null != binder) return binder.BindModel(controllerContext, bindingContext);

            return base.BindModel(controllerContext, bindingContext);
        }
    }

    public interface IModelBinder<T> : IModelBinder
    {

    }

    public abstract class ModelBinder<T> : IModelBinder<T>
    {
        public abstract T BindModel(ControllerContext controllerContext,
                                       ModelBindingContext bindingContext);

        object IModelBinder.BindModel(ControllerContext controllerContext,
                                      ModelBindingContext bindingContext)
        {
            return BindModel(controllerContext, bindingContext);
        }
    }
}
