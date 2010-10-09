using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RavenGallery.Core.Views;
using Raven.Client;
using RavenGallery.Core.Entities;
using RavenGallery.Core.Documents;
using Raven.Client.Indexes;

namespace RavenGallery.Core.Tests.Integration.Views
{
    [TestFixture]
    public class ImageBrowseViewFactoryTests : LocalRavenTest
    {
        public ImageBrowseViewFactory ViewFactory { get; set; }
        public IDocumentSession DocumentSession { get; set; }

        [SetUp]
        public void SetupObjects()
        {
            DocumentSession = this.Store.OpenSession();
            this.ViewFactory = new ImageBrowseViewFactory(this.DocumentSession);
        }

        [Test]
        [TestCase(0)]
        [TestCase(21)]
        public void WhenLoadIsInvokedWithInvalidPageSizePageSizeIsResetToSensibleDefault(int pageSize)
        {
            var results = this.ViewFactory.Load(new ImageBrowseInputModel()
            {
                Page = 0,
                PageSize = pageSize
            });

            Assert.AreEqual(20, results.PageSize);
        }

        [Test]
        [TestCase("Title", 20)]
        [TestCase("Title5", 11)] // 5 and 50,51,52,53 etc
        [TestCase("tag5", 11)]   // Ditto above
        public void WhenLoadIsInvokedWithSearch_ExpectedResultsAreReturned(string searchText, int count)
        {
            PopulateStore();
            var result = this.ViewFactory.Load(new ImageBrowseInputModel()
            {
                Page = 0,
                PageSize = 100,
                SearchText = searchText
            }).Items.Count();
            WaitForIndexing();

            Assert.AreEqual(count, result);
        }

        [Test]
        [TestCase("Tag2")]
        [TestCase("Tag1")]
        public void WhenLoadIsInvoked_OnlyOneResultPerDocumentShouldBeReturned(string searchText)
        {
            DocumentSession.Store(
                  new ImageDocument()
                  {
                      Id = "Test",
                      Tags = new List<ImageTagDocument>()
                        {
                             new ImageTagDocument(){
                                 Name = "Tag2"
                             },
                             new ImageTagDocument(){
                                 Name = "Tag1"
                             },
                        }
                  });
            DocumentSession.SaveChanges();
            WaitForIndexing();

            var result = this.ViewFactory.Load(new ImageBrowseInputModel()
            {
                Page = 0,
                PageSize = 100,
                SearchText =  searchText
            }).Items.Count();
    
            Assert.AreEqual(1, result);
        }


        [Test]
        [TestCase(0, 10)]
        [TestCase(5, 10)]
        [TestCase(2, 5)]
        public void WhenLoadIsInvokedWithSuppliedArgumentsExpectedResultsAreReturned(int page, int pageSize)
        {
            PopulateStore();
            var results = this.ViewFactory.Load(new ImageBrowseInputModel()
            {
                 Page = page,
                 PageSize = pageSize
            });

            Assert.AreEqual(pageSize, results.Items.Count());
            int pageStart = page * pageSize;
            for (int x = pageStart; x < pageStart + pageSize; x++)
            {
                var item = results.Items.ElementAt(x - pageStart);
                Assert.True(String.Compare(string.Format("Title{0}", x),item.Title, false) == 0);
                Assert.True(String.Compare(string.Format("Filename{0}", x), item.Filename, false) == 0);
            }
        }

        private void PopulateStore()
        {
 	        for(int x = 0; x < 100 ; x++){
                DocumentSession.Store(
                    new ImageDocument()
                    {
                        Title = string.Format("Title{0}", x),
                        Filename = string.Format("Filename{0}", x),                        
                        Tags = new List<ImageTagDocument>()
                        {
                             new ImageTagDocument(){
                                 Name = string.Format("tag{0}", x)
                             }
                        }
                    });
            }
            DocumentSession.SaveChanges();
            DocumentSession.Advanced.Clear();
            WaitForIndexing();
        }

        [TearDown]
        public void DestroyObjects()
        {
            this.DocumentSession.Dispose();
        }
    }
}
