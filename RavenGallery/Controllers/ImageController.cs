using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RavenGallery.ViewModels;
using RavenGallery.Core;
using RavenGallery.Core.Commands;
using System.IO;

namespace RavenGallery.Controllers
{
    [HandleError]
    public class ImageController : Controller
    {
        private ICommandInvoker commandInvoker;

        public ImageController(ICommandInvoker commandInvoker)
        {
            this.commandInvoker = commandInvoker;
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult New()
        {
            return View(new ImageNewViewModel());
        }

        [Authorize]
        [HttpPost]
        public ActionResult New(HttpPostedFileBase file, ImageNewViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    byte[] allData = new byte[file.ContentLength];
                    file.InputStream.Read(allData, 0, allData.Length);
                    commandInvoker.Execute(new UploadUserImageCommand(User.Identity.Name, model.Title, model.Tags, allData));
                    return RedirectToAction("Browse");
                }
                else
                {
                    this.ModelState.AddModelError("file", "Please select a file for upload");
                }
            }            
            return View(model);
        }

        [Authorize]
        public ActionResult Edit()
        {
            return View();
        }

   }
}
