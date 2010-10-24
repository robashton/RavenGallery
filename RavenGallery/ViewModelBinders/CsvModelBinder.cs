using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RavenGallery.Core.Web;

namespace RavenGallery.ViewModelBinders
{
    public class CsvModelBinder : ModelBinder<string[]>
    {
        public override string[] BindModel(System.Web.Mvc.ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            return bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue.Split(
                new[] {','},
                StringSplitOptions.RemoveEmptyEntries);
        }
    }
}