using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenGallery.Core.Views
{
    public class ImageBrowseView
    {
        public int PageSize
        {
            get;
            private set;
        }

        public int Page
        {
            get;
            private set;
        }

        public string SearchText
        {
            get;
            private set;
        }

        public IEnumerable<ImageBrowseItem> Items
        {
            get;
            private set;
        }

        public ImageBrowseView(int page, int pageSize, string searchText, IEnumerable<ImageBrowseItem> items)
        {
            this.Page = page;
            this.PageSize = pageSize;
            this.Items = items;
            this.SearchText = searchText;
        }
    }
}
