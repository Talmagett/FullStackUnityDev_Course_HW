using System;
using System.ComponentModel;
using NUnit.Framework;

namespace Converter.Tests
{
    public class ConverterTests
    {
        private Converter _converter;
        private const int LoadLimit = 5;
        private const int UnloadLimit = 5;
        private const int LoadingCount = 3;
        private const int UnloadingCount = 3;
        private const int ConvertingTime = 5;

        [SetUp]
        public void Setup()
        {
            var recipe = new Recipe(ResourceType.Wood, 2, ResourceType.Board, 1);
            var loadZone = new ConverterZone(recipe.InputResourceType, LoadLimit, LoadingCount);
            var unloadZone = new ConverterZone(recipe.OutputResourceType, UnloadLimit, UnloadingCount);
            _converter = new Converter(recipe, ConvertingTime, loadZone, unloadZone);
        }

        [Test]
        public void InitializeConverter()
        {
            Assert.IsNotNull(_converter);
            Assert.IsFalse(_converter.IsWorking);
        }

        [TestCase(1, 1, 0)]
        [TestCase(5, 5, 0)]
        [TestCase(6, 5, 1)]
        [TestCase(10, 5, 5)]
        public void LoadResourcesWithChange(int addingResourcesCount, int expectedCount, int change)
        {
            var resource = new Resource(ResourceType.Wood);
            var gainedChange = _converter.LoadResources(resource, addingResourcesCount);

            Assert.AreEqual(expectedCount, _converter.LoadZone.GetResourcesCount());
            Assert.AreEqual(change, gainedChange);
        }

        [Test]
        public void LoadResourcesWithNullResourceThrowsException()
        {
            Assert.Catch<ArgumentNullException>(() => _converter.LoadResources(null, 1));
        }

        [Test]
        public void LoadResourcesWithDifferentTypeResourceThrowsException()
        {
            var resource = new Resource(ResourceType.Stone);
            Assert.Catch<InvalidEnumArgumentException>(() => _converter.LoadResources(resource, 1));
        }

        [Test]
        public void LoadResourcesWithWrongNumberThrowsException()
        {
            var resource = new Resource(ResourceType.Wood);
            Assert.Catch<ArgumentOutOfRangeException>(() => _converter.LoadResources(resource, 0));
            Assert.Catch<ArgumentOutOfRangeException>(() => _converter.LoadResources(resource, -1));
        }

        [Test]
        public void UpdateConverterWhenEmpty()
        {
            _converter.Update(1);
            Assert.IsFalse(_converter.IsWorking);
        }

        [Test]
        public void UpdateConverterWithLoading()
        {
            var resource = new Resource(ResourceType.Wood);
            _converter.LoadResources(resource, 1);
            _converter.Update(1);
            Assert.IsTrue(_converter.IsWorking);
        }

        [Test]
        public void TurnOffConverter()
        {
            _converter.TurnOff();
            Assert.IsFalse(_converter.IsWorking);
        }

        [Test]
        public void TurnOffConverterWhenWorking()
        {
            var resource = new Resource(ResourceType.Wood);
            _converter.LoadResources(resource, 1);
            _converter.Update(1);
            Assert.IsTrue(_converter.IsWorking);

            _converter.TurnOff();
            Assert.IsFalse(_converter.IsWorking);
        }

        [Test]
        public void TurnOffConverterWhenWorkingWithReturning()
        {
            var resource = new Resource(ResourceType.Wood);
            _converter.LoadResources(resource, 10);
            Assert.AreEqual(_converter.LoadZone.GetResourcesCount(), 5);
            _converter.Update(1);
            Assert.AreEqual(_converter.LoadZone.GetResourcesCount(), 2);

            _converter.TurnOff();
            Assert.AreEqual(_converter.LoadZone.GetResourcesCount(), 5);
        }


        [Test]
        public void TurnOffConverterWhenWorkingWithReturningAndBurning()
        {
            var resource = new Resource(ResourceType.Wood);
            _converter.LoadResources(resource, 10);
            Assert.AreEqual(_converter.LoadZone.GetResourcesCount(), 5);
            _converter.Update(1);
            _converter.LoadResources(resource, 10);
            Assert.AreEqual(_converter.LoadZone.GetResourcesCount(), 5);

            _converter.TurnOff();
            Assert.AreEqual(_converter.LoadZone.GetResourcesCount(), 5);
        }

        [Test]
        public void UpdateConverterFinishingConvert()
        {
            var resource = new Resource(ResourceType.Wood);
            _converter.LoadResources(resource, 5);
            Assert.AreEqual(_converter.LoadZone.GetResourcesCount(), 5);
            Assert.AreEqual(_converter.UnloadZone.GetResourcesCount(), 0);
            _converter.Update(10f);
            Assert.IsFalse(_converter.IsWorking);
            Assert.AreEqual(_converter.LoadZone.GetResourcesCount(), 2);
            Assert.AreEqual(_converter.UnloadZone.GetResourcesCount(), 3);
        }
    }
}