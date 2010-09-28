using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using StructureMap;

namespace RavenGallery.Core.Tests
{
    [TestFixture]
    public class CoreRegistryTests : LocalRavenTest
    {
        [Test]
        public void TestConfiguration()
        {
            ObjectFactory.Initialize(config =>
            {
                config.AddRegistry(new CoreRegistry(this.Store));
            });
            ObjectFactory.AssertConfigurationIsValid();
        }
    }
}
