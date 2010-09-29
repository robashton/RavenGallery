using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace RavenGallery.ViewModels
{
    public class ImageNewViewModel
    {
        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Tags")]
        public string[] Tags { get; set; }

        public ImageNewViewModel()
        {
            Tags = new string[] { };
        }
    }
}