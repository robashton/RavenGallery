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
    public class ImageByRelatedImageViewFactoryTests : LocalRavenTest
    {
        [SetUp]
        public void PopulateData()
        {
            using (var s = this.Store.OpenSession())
            {
                s.Store(new ImageDocument()
                {
                    Id = "knownId",
                    Tags = new List<ImageTagDocument>()
                    {
                        new ImageTagDocument(){ Name = "animal" },
                        new ImageTagDocument() { Name = "dog" }
                    }
                });

                s.Store(new ImageDocument()
                {
                    Id = "expectedId",
                    Tags = new List<ImageTagDocument>()
                    {
                        new ImageTagDocument() { Name= "bull"},
                        new ImageTagDocument() { Name = "dog"}
                    }
                });

                s.Store(new ImageDocument()
                {
                    Id = "unexpectedId",
                    Tags = new List<ImageTagDocument>()
                    {
                        new ImageTagDocument() { Name= "bull"},
                        new ImageTagDocument() { Name = "faeces"}
                    }
                });
                s.SaveChanges();
                WaitForIndexing();
            }
            
        }

        [Test]
        public void WhenLoadIsInvokedWithValidInput()
        {
            using (var s = this.Store.OpenSession())
            {
                ImageByRelatedImageViewFactory factory = new ImageByRelatedImageViewFactory(
                    s);

                var results = factory.Load(new ImageByRelatedImageInputModel() { ImageId = "knownId" });

                Assert.AreEqual(2, results.Items.Length);
            }
        }
    }
}
