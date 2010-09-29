using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core.Utility
{
    public static class IdUtil
    {
        public static string CreateUserId(string username)
        {
            return string.Format("users/{0}", username);
        }
    }
}
