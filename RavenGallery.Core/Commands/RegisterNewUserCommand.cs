using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core.Commands
{
    public class RegisterNewUserCommand
    {
        public string Username
        {
            get;
            private set;
        }

        public string Password
        {
            get;
            private set;
        }

        public RegisterNewUserCommand(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
