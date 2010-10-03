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
    public class ImageTags_GroupByTagName : AbstractIndexCreationTask
    {
        public override Raven.Database.Indexing.IndexDefinition CreateIndexDefinition()
        {
            return new IndexDefinition<ImageDocument, ImageTagCollectionItem>()
            {
                Map = docs => from doc in docs
                              from tag in doc.Tags
                              select new
                              {
                                  tag.Name
                              },
                Reduce = results => from result in results
                                    group result by result.Name into g
                                 select new
                                 {
                                     Name = g.Key,
                                     Count = g.Count()
                                 },
             Indexes = {{x=>x.Name, FieldIndexing.Analyzed}}

            }.ToIndexDefinition(this.DocumentStore.Conventions);
        }
    }
}
