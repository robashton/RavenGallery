using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core.Security
{
    public interface IAuthenticationService
    {
        void SignIn(string username, bool persistent);
        void SignOut();
    }
}
