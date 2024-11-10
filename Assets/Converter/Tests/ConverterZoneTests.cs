using System;
using NUnit.Framework;

namespace Converter.Tests
{
    public class ConverterZoneTests
    {
        [Test]
        public void Instantiate()
        {
            var converterZone = new ConverterZone(5,ResourceType.Wood);
            Assert.IsNotNull(converterZone);
            Assert.AreEqual(0, converterZone.Count);
            Assert.AreEqual(ResourceType.Wood, converterZone.ResourceType);
        }

        [TestCase(1,0)]
        [TestCase(4,0)]
        [TestCase(5,0)]
        [TestCase(6,1)]
        [TestCase(10,5)]
        public void AddResource(int count, int extra)
        {
            var limit = 5;
            var converterZone = new ConverterZone(limit, ResourceType.Wood);
            var extraOutput = converterZone.AddResources(count);
            Assert.AreEqual(extraOutput, extra);
            Assert.AreEqual(extraOutput>0?limit:count, converterZone.Count);
        }
        
        [TestCase(-1)]
        [TestCase(0)]
        public void AddResourcesOutOfRangeThrowException(int count)
        {
            var converterZone = new ConverterZone(5, ResourceType.Wood);
            Assert.Catch<ArgumentOutOfRangeException>(()=>converterZone.AddResources(count));
        }
    }
}