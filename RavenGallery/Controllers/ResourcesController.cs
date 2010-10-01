using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RavenGallery.Core.Infrastructure;

namespace RavenGallery.Controllers
{
    [HandleError]
    public class ResourcesController : Controller
    {
        private IFileStorageService fileStorageService;

        public ResourcesController(IFileStorageService fileStorageService)
        {
            this.fileStorageService = fileStorageService;
        }

        public ActionResult Image(string filename)
        {
            Byte[] fileBytes = fileStorageService.RetrieveFile(filename);
            return File(fileBytes, "image/jpeg");           
        }
    }
}
