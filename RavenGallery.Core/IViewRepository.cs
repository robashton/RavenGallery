using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core
{
    public interface IViewRepository
    {
        TOutput Load<TInput, TOutput>(TInput input);
    }
}
