using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RavenGallery.ViewModels;
using RavenGallery.Core;
using RavenGallery.Core.Commands;
using System.IO;
using RavenGallery.Core.Views;
using RavenGallery.Core.Web;
using RavenGallery.InputModels;

namespace RavenGallery.Controllers
{
    [HandleError]
    public class ImageController : Controller
    {
        private ICommandInvoker commandInvoker;
        private IViewRepository viewRepository;

        public ImageController(ICommandInvoker commandInvoker, IViewRepository viewRepository)
        {
            this.commandInvoker = commandInvoker;
            this.viewRepository = viewRepository;
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult New()
        {
            return View(new ImageNewViewModel());
        }

        [Authorize]
        [UserIdFilter]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult New(string currentUserId, HttpPostedFileBase file, ImageNewViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    byte[] allData = new byte[file.ContentLength];
                    file.InputStream.Read(allData, 0, allData.Length);
                    commandInvoker.Execute(new UploadUserImageCommand(currentUserId, model.Title, model.Tags, allData));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    this.ModelState.AddModelError("file", "Please select a file for upload");
                }
            }            
            return View(model);
        }

        [ActionName("View")]
        public ActionResult ViewImage()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _UpdateImageTitle(string imageId, UpdateImageTitleInput input)
        {
            if (ModelState.IsValid)
            {
                commandInvoker.Execute(new UpdateImageTitleCommand(
                    imageId, input.Title));
                return Json(new { Success = true });
            }
            else
            {
                return Json(new { Success = false });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _UpdateImageTags(string imageId, UpdateImageTagsInput input)
        {
            if (ModelState.IsValid)
            {
                commandInvoker.Execute(new UpdateImageTagsCommand(
                    imageId, input.Tags));
                return Json(new { Success = true });
            }
            else
            {
                return Json(new { Success = false });
            }
        }

        public ActionResult _GetImage(ImageViewInputModel input)
        {
            var model = viewRepository.Load<ImageViewInputModel, ImageView>(input);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _GetBrowseData(ImageBrowseInputModel input)
        {
            var model = viewRepository.Load<ImageBrowseInputModel, ImageBrowseView>(input);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _GetTags(ImageTagCollectionInputModel input)
        {
            var model = viewRepository.Load<ImageTagCollectionInputModel, ImageTagCollectionView>(input);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
   }
}
