using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RavenGallery.ViewModels;
using RavenGallery.Core.Web;

namespace RavenGallery.ViewModelBinders
{
    public class ImageNewViewModelBinder : ModelBinder<ImageNewViewModel>
    {
        public override ImageNewViewModel BindModel(System.Web.Mvc.ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            return new ImageNewViewModel()
            {
                Title = bindingContext.ValueProvider.GetValue("Title").AttemptedValue,
                Tags = bindingContext.ValueProvider.GetValue("Tags").AttemptedValue.Split(',')
            };
        }
    }
}