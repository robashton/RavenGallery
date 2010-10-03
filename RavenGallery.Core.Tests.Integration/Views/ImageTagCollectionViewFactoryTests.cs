using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Raven.Client;
using RavenGallery.Core.Views;
using RavenGallery.Core.Documents;

namespace RavenGallery.Core.Tests.Integration.Views
{
    [TestFixture]
    public class ImageTagCollectionViewFactoryTests : LocalRavenTest
    {
        public ImageTagCollectionViewFactory ViewFactory { get; set; }
        public IDocumentSession DocumentSession { get; set; }

        [SetUp]
        public void CreateObjects()
        {
            DocumentSession = this.Store.OpenSession();
            ViewFactory = new ImageTagCollectionViewFactory(DocumentSession);
        }

        [TearDown]
        public void DestroyObjects()
        {
            DocumentSession.Dispose();
        }

        [Test]
        [TestCase("So", 4)]
        [TestCase("SomeTag3", 1)]
        [TestCase("SomeO", 2)]
        [TestCase("Ano", 1)]
        public void WhenLoadIsInvokedWithSearchText_ExpectedNumberOfResultsAreReturned(string searchTerm, int expectedCount)
        {
            PopulateData();
            var results = ViewFactory.Load(new ImageTagCollectionInputModel() { SearchText = searchTerm });
            Assert.AreEqual(expectedCount, results.Items.Count());
        }

        [Test]
        [TestCase("SomeTag1", 1)]
        [TestCase("SomeTag3", 1)]
        [TestCase("SomeOtherTag1", 2)]
        [TestCase("SomeOtherTag2", 2)]
        [TestCase("AnotherTagEntirely", 1)]
        public void WhenLoadIsInvokedWithNoSearchText_ModelContainsItemsWithCorrectInstanceCounts(string searchTerm, int expectedCount)
        {
            PopulateData();
            var results = ViewFactory.Load(new ImageTagCollectionInputModel());
            
            // TODO: This is actually compensating for a bug in the unstable fork of RavenDB
            // And should be replaced with a case sensitive match when that has been repaired
            var specificResult = results.Items.Where(x => String.Compare(x.Name, searchTerm, true) == 0).FirstOrDefault();
            Assert.AreEqual(expectedCount, specificResult.Count);
        }


        private void PopulateData()
        {
            DocumentSession.Store(new ImageDocument()
            {
                Tags = new List<ImageTagDocument>()
                {
                    new ImageTagDocument() { Name = "SomeTag1" },
                    new ImageTagDocument() { Name = "SomeTag3" },
                }
            });
            DocumentSession.Store(new ImageDocument()
            {
                Tags = new List<ImageTagDocument>()
                {
                    new ImageTagDocument() { Name = "SomeOtherTag1" },
                    new ImageTagDocument() { Name = "SomeOtherTag2" },
                }
            });
            DocumentSession.Store(new ImageDocument()
            {
                Tags = new List<ImageTagDocument>()
                {
                    new ImageTagDocument() { Name = "AnotherTagEntirely" }
                }
            });
            DocumentSession.Store(new ImageDocument()
            {
                Tags = new List<ImageTagDocument>()
                {
                    new ImageTagDocument() { Name = "SomeOtherTag1" },
                    new ImageTagDocument() { Name = "SomeOtherTag2" },
                }
            });

            DocumentSession.SaveChanges();
            WaitForIndexing();
        }
    }
}
