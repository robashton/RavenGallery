using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RavenGallery.Controllers
{
    [HandleError]
    public class SharedController : Controller
    {
        [Authorize]
        public ActionResult Error()
        {
            return View();
        }
    }
}
