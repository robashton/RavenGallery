using System.Web;
using System.Web.Util;

namespace RavenGallery.Core.Infrastructure
{
    public class RavenGalleryRequestValidator : RequestValidator
    {
        protected override bool IsValidRequestString(HttpContext context, string value, RequestValidationSource requestValidationSource, string collectionKey, out int validationFailureIndex)
        {
            validationFailureIndex = -1;
            return true;
        }
    }
}


