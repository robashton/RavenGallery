using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RavenGallery.Core.Entities;
using RavenGallery.Core.Documents;

namespace RavenGallery.Core.Tests.Entities
{
    [TestFixture]
    public class ImageEntityTests
    {
        [Test]
        public void AddTag_AddsTagToDocument()
        {
            ImageDocument innerDocument = new ImageDocument();
            Image image = new Image(innerDocument);
            image.AddTag("tag");

            Assert.AreEqual(innerDocument.Tags[0].Name, "tag");
        }
    }
}
