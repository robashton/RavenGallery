using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RavenGallery.Core.Infrastructure;

namespace RavenGallery.Core.Tests.Integration.Infrastructure
{
    [TestFixture]
    public class RavenFileStorageServiceTests : LocalRavenTest
    {
        [Test]
        public void WhenStoreFileInvokedWithValidArguments_AttachmentIsCreatedInRaven()
        {
            Byte[] fileBytes = new Byte[] { 3,1,4,1,5,9};
            String filename = "images/mySuperDuperFile";
            RavenFileStorageService storage = new RavenFileStorageService(this.Store);

            storage.StoreFile(filename, fileBytes);

            var retrievedAttachment = this.Store.DatabaseCommands.GetAttachment(filename);
            Assert.AreEqual(fileBytes, retrievedAttachment.Data);
        }
    }
}
