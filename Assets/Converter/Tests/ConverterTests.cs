using Converter;
using NUnit.Framework;

namespace Converter.Tests
{
    public class ConverterTests
    {
        private Converter _converter;
        
        [SetUp]
        public void Setup()
        {
            var recipe = new Recipe(ResourceType.Wood,2,ResourceType.Board,1);
            var loadLimit = 5;
            var unloadLimit = 5;
            var convertingTime = 5;
            _converter = new Converter(recipe, convertingTime, loadLimit, unloadLimit);
        }

        [Test]
        public void InitializeConverter()
        {
            Assert.IsNotNull(_converter);
            Assert.IsFalse(_converter.IsWorking);
        }

        [TestCase(1,true)]
        [TestCase(5,true)]
        [TestCase(6,false)]
        [TestCase(10,false)]
        public void AddResourceWhenEmpty(int addingResourcesCount, bool expectedSuccess)
        {
            var resource = new Resource(ResourceType.Wood); 
            var success = _converter.LoadResources(resource,addingResourcesCount);
            
            Assert.IsTrue(_converter.IsWorking);
            Assert.AreEqual(success, expectedSuccess);
        }
    }
}