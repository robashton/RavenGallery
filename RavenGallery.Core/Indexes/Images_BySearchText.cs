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
            return new IndexDefinition<ImageDocument, IndexResult>()
            {
                Map = docs => from doc in docs
                                from tag in doc.Tags
                                select new
                                {
                                    doc.Title,
                                    doc.Filename,
                                    tag.Name
                                },
                Indexes = {
                    { x=> x.Title, FieldIndexing.Analyzed },
                    { x => x.Name, FieldIndexing.Analyzed },
                    { x => x.Filename, FieldIndexing.No },
                },
                Stores =
                {
                    { x=> x.Title, FieldStorage.Yes },
                    { x=> x.Filename, FieldStorage.Yes },
                }
            }
            .ToIndexDefinition(DocumentStore.Conventions);
        }

        private class IndexResult
        {
            public string Title { get; set; }
            public string Name { get; set; }
            public string Filename { get; set;}
        }
    }
}
