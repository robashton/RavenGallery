using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RavenGallery.ViewModels;
using RavenGallery.Core;
using RavenGallery.Core.Commands;
using System.Web.Security;
using RavenGallery.Core.Security;

namespace RavenGallery.Controllers
{
    [HandleError]
    public class UserController : Controller
    {
        private ICommandInvoker commandInvoker;
        private IAuthenticationService authenticationService;

        public UserController(ICommandInvoker commandInvoker, IAuthenticationService authenticationService)
        {
            this.commandInvoker = commandInvoker;
            this.authenticationService = authenticationService;

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Register()
        {
            return View(new UserRegisterViewModel());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SignIn()
        {
            return View(new UserSignInViewModel());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SignOut()
        {
            authenticationService.SignOut();
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                commandInvoker.Execute(new RegisterNewUserCommand(model.Username, model.Password));
                authenticationService.SignIn(model.Username, model.StayLoggedIn);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }            
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SignIn(UserSignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                authenticationService.SignIn(model.Username, model.StayLoggedIn);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }          
        }
    }
}
