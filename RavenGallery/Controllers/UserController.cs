using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RavenGallery.ViewModels;
using RavenGallery.Core;
using RavenGallery.Core.Commands;

namespace RavenGallery.Controllers
{
    [HandleError]
    public class UserController : Controller
    {
        private ICommandInvoker commandInvoker;

        public UserController(ICommandInvoker commandInvoker)
        {
            this.commandInvoker = commandInvoker;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Register()
        {
            return View(new UserRegisterViewModel());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Send command
                commandInvoker.Execute(new RegisterNewUserCommand(model.Username, model.Password));

                // Go back to home
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Return back to the page
                return View(model);
            }            
        }
    }
}
