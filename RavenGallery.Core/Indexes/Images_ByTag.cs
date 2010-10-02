using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Indexes;
using RavenGallery.Core.Documents;

namespace RavenGallery.Core.Indexes
{
    public class Images_ByTag : AbstractIndexCreationTask
    {
        public override Raven.Database.Indexing.IndexDefinition CreateIndexDefinition()
        {
            return new IndexDefinition<ImageDocument>()
            {
                Map = docs => from doc in docs
                              from tag in doc.Tags
                              select new
                              {
                                  tag.Name
                              }
            }
            .ToIndexDefinition(DocumentStore.Conventions);
        }
    }
}
