using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core
{
    public interface ICommandInvoker
    {
        void Execute<T>(T command);
    }
}
