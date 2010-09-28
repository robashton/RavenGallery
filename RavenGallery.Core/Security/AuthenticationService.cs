using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace RavenGallery.Core.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        public void SignIn(string username, bool persistent)
        {
            FormsAuthentication.SetAuthCookie(username, persistent);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
