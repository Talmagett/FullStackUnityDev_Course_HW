using System;
using NUnit.Framework;

namespace Converter.Tests
{
    public class ConverterZoneTests
    {
        [Test]
        public void Instantiate()
        {
            const ResourceType resourceType = ResourceType.Wood;
            var converterZone = new ConverterZone(resourceType,5,1);
            
            Assert.IsNotNull(converterZone);
            Assert.AreEqual(0, converterZone.GetResourcesCount());
            Assert.IsTrue(converterZone.CanConvert(resourceType));
        }

        [TestCase(1,0)]
        [TestCase(4,0)]
        [TestCase(5,0)]
        [TestCase(6,1)]
        [TestCase(10,5)]
        public void AddResourcesToZone(int count, int change)
        {
            var limit = 5;
            var converterZone = new ConverterZone(ResourceType.Wood,5,1);
            var changeOutput = converterZone.AddResources(count);
            Assert.AreEqual(changeOutput, change);
            Assert.AreEqual(changeOutput>0?limit:count, converterZone.GetResourcesCount());
        }
        
        [TestCase(-1)]
        [TestCase(0)]
        public void AddResourcesToZoneOutOfRangeThrowException(int count)
        {
            var converterZone = new ConverterZone(ResourceType.Wood,5,1);
            Assert.Catch<ArgumentOutOfRangeException>(()=>converterZone.AddResources(count));
        }
    }
}