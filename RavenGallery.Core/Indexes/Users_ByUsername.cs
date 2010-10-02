using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Indexes;
using RavenGallery.Core.Documents;

namespace RavenGallery.Core.Indexes
{
    public class Users_ByUsername : AbstractIndexCreationTask
    {
        public override Raven.Database.Indexing.IndexDefinition CreateIndexDefinition()
        {
            return new IndexDefinition<UserDocument>()
            {
                Map = docs => from doc in docs
                              select new
                              {
                                  doc.Username
                              }
            }
            .ToIndexDefinition(DocumentStore.Conventions);
        }
    }
}
