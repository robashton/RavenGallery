using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Indexes;
using RavenGallery.Core.Documents;
using RavenGallery.Core.Views;
using Raven.Database.Indexing;

namespace RavenGallery.Core.Indexes
{
    public class Images_ByTag : AbstractIndexCreationTask
    {
        public override Raven.Database.Indexing.IndexDefinition CreateIndexDefinition()
        {
            return new IndexDefinition<ImageDocument, ImageBrowseView>()
            {
                Map = docs => from doc in docs
                              from tag in doc.Tags
                              select new
                              {
                                  tag.Name
                              },
                Indexes = {{ x=> x.SearchText, FieldIndexing.Analyzed }}
            }
            .ToIndexDefinition(DocumentStore.Conventions);
        }
    }
}
