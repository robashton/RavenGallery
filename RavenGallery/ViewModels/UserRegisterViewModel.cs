using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace RavenGallery.ViewModels
{
    public class UserRegisterViewModel
    {
        [DisplayName("Username")]
        public string Username
        {
            get;
            set;
        }

         [DisplayName("Password")]
        public string Password
        {
            get;
            set;
        }

        [DisplayName("Stay logged in")]
        public Boolean StayLoggedIn
        {
            get;
            set;
        }
    }
}