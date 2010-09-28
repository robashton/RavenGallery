using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core.Documents
{
    public class UserDocument
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
