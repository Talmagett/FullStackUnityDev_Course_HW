using System;
using NUnit.Framework;

namespace Converter.Tests
{
    public class ConverterZoneTests
    {
        [Test]
        public void Instantiate()
        {
            var converterZone = new ConverterZone(5);
            
            Assert.IsNotNull(converterZone);
        }
        
        [TestCase(0)]
        [TestCase(-1)]
        public void InstantiateWithLimitNonNaturalNumberThenThrowException(int limit)
        {
            Assert.Catch<ArgumentOutOfRangeException>(() =>
            {
                var converterZone = new ConverterZone(limit);
            });
        }
        
        [TestCase(5,1,1,0)]
        [TestCase(5,4,4,0)]
        [TestCase(5,5,5,0)]
        [TestCase(5,6,5,1)]
        [TestCase(5,10,5,5)]
        public void AddResourcesToZone(int limit, int addingCount,  int expectedCount, int expectedChange)
        {
            var converterZone = new ConverterZone(limit);
            var changeOutput = converterZone.PutResources(addingCount);
            Assert.AreEqual(expectedChange,changeOutput);
            Assert.AreEqual(expectedCount,converterZone.GetResourcesCount());
        }
        
        [TestCase(3,0)]
        [TestCase(3,-1)]
        public void AddResourcesToZoneOutOfRangeThrowException(int limit, int count)
        {
            var converterZone = new ConverterZone(limit);
            Assert.Catch<ArgumentOutOfRangeException>(()=>converterZone.PutResources(count));
        }

        [TestCase(5,5,3,2,3)]
        [TestCase(5,5,7,0,5)]
        public void RemoveResourcesFromZone(int limit, int addingCount, int removingCount, int expectedCount,int expectedRemovedCount)
        {
            var converterZone = new ConverterZone(limit);
            converterZone.PutResources(addingCount);
            var removedResources =converterZone.RemoveResources(removingCount);
            Assert.AreEqual(expectedCount,converterZone.GetResourcesCount());
            Assert.AreEqual(expectedRemovedCount,removedResources);
        }

        [TestCase(5,5,0,5)]
        public void RemoveResourcesFromZoneWithOutOfRangeException(int limit, int addingCount, int removingCount, int expectedCount)
        {
            var converterZone = new ConverterZone(limit);
            converterZone.PutResources(addingCount);
            Assert.Catch<ArgumentOutOfRangeException>(() =>
            {
                converterZone.RemoveResources(removingCount);
            });
        }
    }
}