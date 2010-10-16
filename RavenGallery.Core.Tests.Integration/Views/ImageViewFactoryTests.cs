using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RavenGallery.Core.Documents;
using RavenGallery.Core.Views;

namespace RavenGallery.Core.Tests.Integration.Views
{
    [TestFixture]
    public class ImageViewFactoryTests : LocalRavenTest
    {
        [Test]
        public void WhenLoadIsInvoked_DocumentIsFlattenedIntoView()
        {
            ImageDocument doc = new ImageDocument()
            {
                Id = "knownId",
                Filename = "filename",
                Title = "title",
                Tags = new List<ImageTagDocument>()
                {
                    new ImageTagDocument(){
                         Name = "tagOne"
                    },
                    new ImageTagDocument(){
                        Name = "tagTwo"
                    }
                }
            };

            using (var s = Store.OpenSession())
            {
                s.Store(doc);
                s.SaveChanges();
            }

            using (var s = Store.OpenSession())
            {
                ImageViewFactory factory = new ImageViewFactory(s);
                var results = factory.Load(new ImageViewInputModel()
                {
                     ImageId = "knownId"
                });

                Assert.AreEqual("title", results.Title);
                Assert.AreEqual("filename", results.Filename);
                Assert.AreEqual(new[] { "tagOne", "tagTwo" }, results.Tags);
            }

        }
    }
}
