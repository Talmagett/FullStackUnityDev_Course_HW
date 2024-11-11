using System;
using System.ComponentModel;
using NUnit.Framework;
using UnityEngine;

namespace Converter.Tests
{
    public class ConverterTests
    {
        [Test]
        public void InitializeConverter()
        {
            //Arrange:
            var recipe = new Recipe(ResourceType.Wood, 1, ResourceType.Board, 1);
            var loadZone = new ConverterZone(recipe.InputResourceType, 5, 3);
            var unloadZone = new ConverterZone(recipe.OutputResourceType, 5, 3);
            var converter = new Converter(recipe, 3, loadZone, unloadZone);
            
            //Assert:
            Assert.IsNotNull(converter);
            Assert.IsFalse(converter.IsWorking);
        }

        [TestCase(1, 1, 0)]
        [TestCase(5, 5, 0)]
        [TestCase(6, 5, 1)]
        [TestCase(10, 5, 5)]
        public void LoadResourcesWithChange(int addingResourcesCount, int expectedCount, int change)
        {
            //Arrange:
            var recipe = new Recipe(ResourceType.Wood, 1, ResourceType.Board, 1);
            var loadZone = new ConverterZone(recipe.InputResourceType, 5, 3);
            var unloadZone = new ConverterZone(recipe.OutputResourceType, 5, 3);
            var converter = new Converter(recipe, 3, loadZone, unloadZone);
            var resource = new Resource(ResourceType.Wood);

            //Act:
            var gainedChange = converter.LoadResources(resource, addingResourcesCount);

            //Assert:
            Assert.AreEqual(expectedCount, converter.LoadZone.GetResourcesCount());
            Assert.AreEqual(change, gainedChange);
        }

        [Test]
        public void LoadResourcesWithNullResourceThrowsException()
        {
            //Arrange:
            var recipe = new Recipe(ResourceType.Wood, 1, ResourceType.Board, 1);
            var loadZone = new ConverterZone(recipe.InputResourceType, 5, 3);
            var unloadZone = new ConverterZone(recipe.OutputResourceType, 5, 3);
            var converter = new Converter(recipe, 3, loadZone, unloadZone);
            
            //Assert:
            Assert.Catch<ArgumentNullException>(() => converter.LoadResources(null, 1));
        }

        [Test]
        public void LoadResourcesWithDifferentTypeResourceThrowsException()
        {
            //Arrange:
            var recipe = new Recipe(ResourceType.Wood, 1, ResourceType.Board, 1);
            var loadZone = new ConverterZone(recipe.InputResourceType, 5, 3);
            var unloadZone = new ConverterZone(recipe.OutputResourceType, 5, 3);
            var converter = new Converter(recipe, 3, loadZone, unloadZone);
            var resource = new Resource(ResourceType.Stone);
            
            //Assert:
            Assert.Catch<InvalidEnumArgumentException>(() => converter.LoadResources(resource, 1));
        }

        [Test]
        public void LoadResourcesWithWrongNumberThrowsException()
        {
            //Arrange:
            var recipe = new Recipe(ResourceType.Wood, 1, ResourceType.Board, 1);
            var loadZone = new ConverterZone(recipe.InputResourceType, 5, 3);
            var unloadZone = new ConverterZone(recipe.OutputResourceType, 5, 3);
            var converter = new Converter(recipe, 3, loadZone, unloadZone);
            var resource = new Resource(ResourceType.Wood);
            
            //Assert:
            Assert.Catch<ArgumentOutOfRangeException>(() => converter.LoadResources(resource, 0));
            Assert.Catch<ArgumentOutOfRangeException>(() => converter.LoadResources(resource, -1));
        }

        [Test]
        public void UpdateConverterWhenEmpty()
        {
            //Arrange:
            var recipe = new Recipe(ResourceType.Wood, 1, ResourceType.Board, 1);
            var loadZone = new ConverterZone(recipe.InputResourceType, 5, 3);
            var unloadZone = new ConverterZone(recipe.OutputResourceType, 5, 3);
            var converter = new Converter(recipe, 3, loadZone, unloadZone);

            //Act:
            converter.Update(1);
            
            //Assert:
            Assert.IsFalse(converter.IsWorking);
        }

        [Test]
        public void UpdateConverterWithLoading()
        {
            //Arrange:
            var recipe = new Recipe(ResourceType.Wood, 1, ResourceType.Board, 1);
            var loadZone = new ConverterZone(recipe.InputResourceType, 5, 3);
            var unloadZone = new ConverterZone(recipe.OutputResourceType, 5, 3);
            var converter = new Converter(recipe, 3, loadZone, unloadZone);
            var resource = new Resource(ResourceType.Wood);
            
            //Act:
            converter.LoadResources(resource, 1);
            converter.Update(1);
            
            //Assert:
            Assert.IsTrue(converter.IsWorking);
        }

        [Test]
        public void TurnOffConverter()
        {
            //Arrange:
            var recipe = new Recipe(ResourceType.Wood, 1, ResourceType.Board, 1);
            var loadZone = new ConverterZone(recipe.InputResourceType, 5, 3);
            var unloadZone = new ConverterZone(recipe.OutputResourceType, 5, 3);
            var converter = new Converter(recipe, 3, loadZone, unloadZone);

            //Act:
            converter.TurnOff();
            
            //Assert:
            Assert.IsFalse(converter.IsWorking);
        }

        [Test]
        public void TurnOffConverterWhenWorking()
        {
            //Arrange:
            var recipe = new Recipe(ResourceType.Wood, 1, ResourceType.Board, 1);
            var loadZone = new ConverterZone(recipe.InputResourceType, 5, 3);
            var unloadZone = new ConverterZone(recipe.OutputResourceType, 5, 3);
            var converter = new Converter(recipe, 3, loadZone, unloadZone);
            var resource = new Resource(ResourceType.Wood);
            
            //Act: Arrange:
            converter.LoadResources(resource, 1);
            converter.Update(1);
            Assert.IsTrue(converter.IsWorking);

            converter.TurnOff();
            Assert.IsFalse(converter.IsWorking);
        }

        [Test]
        public void TurnOffConverterWhenWorkingWithReturning()
        {
            //Arrange:
            var recipe = new Recipe(ResourceType.Wood, 1, ResourceType.Board, 1);
            var loadZone = new ConverterZone(recipe.InputResourceType, 5, 3);
            var unloadZone = new ConverterZone(recipe.OutputResourceType, 5, 3);
            var converter = new Converter(recipe, 3, loadZone, unloadZone);
            var resource = new Resource(ResourceType.Wood);
            
            //Act: Arrange:
            converter.LoadResources(resource, 10);
            Assert.AreEqual(converter.LoadZone.GetResourcesCount(), 5);
            
            converter.Update(1);
            Assert.AreEqual(converter.LoadZone.GetResourcesCount(), 2);
            
            converter.TurnOff();
            Assert.AreEqual(converter.LoadZone.GetResourcesCount(), 5);
        }


        [Test]
        public void TurnOffConverterWhenWorkingWithReturningAndBurning()
        {
            //Arrange:
            var recipe = new Recipe(ResourceType.Wood, 1, ResourceType.Board, 1);
            var loadZone = new ConverterZone(recipe.InputResourceType, 5, 3);
            var unloadZone = new ConverterZone(recipe.OutputResourceType, 5, 3);
            var converter = new Converter(recipe, 3, loadZone, unloadZone);
            var resource = new Resource(ResourceType.Wood);
            
            //Act: Arrange:
            converter.LoadResources(resource, 10);
            Assert.AreEqual(converter.LoadZone.GetResourcesCount(), 5);
            
            converter.Update(1);
            converter.LoadResources(resource, 10);
            Assert.AreEqual(converter.LoadZone.GetResourcesCount(), 5);
            
            converter.TurnOff();
            Assert.AreEqual(converter.LoadZone.GetResourcesCount(), 5);
        }

        [Test]
        public void UpdateConverterFinishingConvert()
        {
            //Arrange:
            var recipe = new Recipe(ResourceType.Wood, 1, ResourceType.Board, 1);
            var loadZone = new ConverterZone(recipe.InputResourceType, 5, 3);
            var unloadZone = new ConverterZone(recipe.OutputResourceType, 5, 3);
            var converter = new Converter(recipe, 3, loadZone, unloadZone);
            var resource = new Resource(ResourceType.Wood);
            
            //Act: Arrange:
            converter.LoadResources(resource, 5);
            Assert.AreEqual(converter.LoadZone.GetResourcesCount(), 5);
            Assert.AreEqual(converter.UnloadZone.GetResourcesCount(), 0);
            
            converter.Update(10f);
            Assert.IsFalse(converter.IsWorking);
            Assert.AreEqual(converter.LoadZone.GetResourcesCount(), 2);
            Assert.AreEqual(converter.UnloadZone.GetResourcesCount(), 3);
        }
        
        [Test]
        public void UpdateConverterFinishingConvertManyResources()
        {
            //Arrange:
            var recipe = new Recipe(ResourceType.Wood, 1, ResourceType.Board, 1);
            var loadZone = new ConverterZone(recipe.InputResourceType, 5, 1);
            var unloadZone = new ConverterZone(recipe.OutputResourceType, 5, 1);
            var converter = new Converter(recipe, 1, loadZone, unloadZone);
            var resource = new Resource(ResourceType.Wood);
            
            //Act: Arrange:
            converter.LoadResources(resource, 5);
            for (int i = 0,j=5; i < j; i++)
            {
                Assert.AreEqual(converter.LoadZone.GetResourcesCount(), j-i);
                Assert.AreEqual(converter.UnloadZone.GetResourcesCount(), i);
            
                converter.Update(1);
            }
            Assert.IsFalse(converter.IsWorking);
        }
    }
}