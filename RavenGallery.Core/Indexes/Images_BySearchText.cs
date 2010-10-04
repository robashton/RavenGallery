using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Indexes;
using RavenGallery.Core.Documents;
using Raven.Database.Indexing;
using RavenGallery.Core.Views;

namespace RavenGallery.Core.Indexes
{
    public class Images_BySearchText : AbstractIndexCreationTask
    {
        public override Raven.Database.Indexing.IndexDefinition CreateIndexDefinition()
        {
            return new IndexDefinition<ImageDocument, ReduceResult>()
            {
                Map = docs => from doc in docs
                              from tag in doc.Tags
                              select new
                              {
                                  doc.Title,
                                  tag.Name
                              },
                Indexes = {
                    { x=> x.Title, FieldIndexing.Analyzed },
                    { x => x.Name, FieldIndexing.Analyzed },
                }
            }
            .ToIndexDefinition(DocumentStore.Conventions);
        }

        private class ReduceResult
        {
            public string Title { get; set; }
            public string Name { get; set; }
        }
    }
}
