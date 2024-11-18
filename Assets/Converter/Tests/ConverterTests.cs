using System;
using System.ComponentModel;
using NUnit.Framework;

namespace Converter.Tests
{
    public class ConverterTests
    {
        [Test]
        public void InitializeConverter()
        {
            //Arrange:
            var recipe = new Recipe(new Wood(), 1, new Board(), 1);
            var converter = new Converter(recipe, 3, 5, 5, 3);

            //Assert:
            Assert.IsNotNull(converter);
            Assert.IsFalse(converter.IsWorking);
            Assert.IsTrue(converter.OfResourceType(new Wood()));
        }

        [TestCase(1, 1, 0)]
        [TestCase(5, 5, 0)]
        [TestCase(6, 5, 1)]
        [TestCase(10, 5, 5)]
        public void LoadResourcesWithChange(int addingResourcesCount, int expectedCount, int change)
        {
            //Arrange:
            var recipe = new Recipe(new Wood(), 1, new Board(), 1);
            var converter = new Converter(recipe, 3, 5, 5, 3);

            //Act:
            var gainedChange = converter.LoadResources(new Wood(), addingResourcesCount);

            //Assert:
            Assert.AreEqual(expectedCount, converter.LoadZone.GetResourcesCount());
            Assert.AreEqual(change, gainedChange);
        }

        [Test]
        public void LoadResourcesWithNullResourceThrowsException()
        {
            //Arrange:
            var recipe = new Recipe(new Wood(), 1, new Board(), 1);
            var converter = new Converter(recipe, 3, 5, 5, 3);

            //Assert:
            Assert.Catch<ArgumentNullException>(() => converter.LoadResources(null, 1));
        }


        [Test]
        public void LoadResourcesWithDifferentTypeResourceThrowsException()
        {
            //Arrange:
            var recipe = new Recipe(new Wood(), 1, new Board(), 1);
            var converter = new Converter(recipe, 3, 5, 5, 3);

            //Assert:
            Assert.Catch<InvalidEnumArgumentException>(() => converter.LoadResources(new Board(), 1));
            Assert.Catch<InvalidEnumArgumentException>(() => converter.LoadResources(new Stone(), 1));
        }

        [Test]
        public void LoadResourcesWithWrongNumberThrowsException()
        {
            //Arrange:
            var recipe = new Recipe(new Wood(), 1, new Board(), 1);
            var converter = new Converter(recipe, 3, 5, 5, 3);

            //Assert:
            Assert.Catch<ArgumentOutOfRangeException>(() => converter.LoadResources(new Wood(), 0));
            Assert.Catch<ArgumentOutOfRangeException>(() => converter.LoadResources(new Wood(), -1));
        }

        [Test]
        public void UpdateConverterWhenEmpty()
        {
            //Arrange:
            var recipe = new Recipe(new Wood(), 1, new Board(), 1);
            var converter = new Converter(recipe, 3, 5, 5, 3);

            //Act:
            converter.Update(1);

            //Assert:
            Assert.IsFalse(converter.IsWorking);
        }

        [Test]
        public void UpdateConverterWithLoading()
        {
            //Arrange:
            var recipe = new Recipe(new Wood(), 1, new Board(), 1);
            var converter = new Converter(recipe, 3, 5, 5, 3);

            //Act:
            converter.LoadResources(new Wood(), 1);
            converter.Update(1);

            //Assert:
            Assert.IsTrue(converter.IsWorking);
            Assert.AreEqual(converter.LoadZone.GetResourcesCount(), 0);
        }

        [Test]
        public void UpdateConverterFinishingConvert()
        {
            //Arrange:
            var recipe = new Recipe(new Wood(), 1, new Board(), 1);
            var converter = new Converter(recipe, 3, 5, 5, 3);

            //Act: Arrange:
            converter.LoadResources(new Wood(), 5);
            Assert.AreEqual(5, converter.LoadZone.GetResourcesCount());
            Assert.AreEqual(0, converter.UnloadZone.GetResourcesCount());

            converter.Update(10f);
            Assert.IsFalse(converter.IsWorking);
            Assert.AreEqual(2, converter.LoadZone.GetResourcesCount());
            Assert.AreEqual(3, converter.UnloadZone.GetResourcesCount());
        }

        [TestCase(2, 1, 1, 2, TestName = "Recipe 2:1")]
        [TestCase(2, 2, 1, 4, TestName = "Recipe 2:2")]
        [TestCase(1, 2, 0, 10, TestName = "Recipe 1:2")]
        [TestCase(2, 3, 1, 6, TestName = "Recipe 2:3")]
        [TestCase(3, 2, 2, 2, TestName = "Recipe 3:2")]
        public void UpdateConverterFinishingConvertWithComplexRecipe(int inputResourceCount, int outputResourceCount, int expectedInputCount, int expectedOutputCount)
        {
            //Arrange:
            var recipe = new Recipe(new Wood(), inputResourceCount, new Board(), outputResourceCount);
            var converter = new Converter(recipe, 3, 20, 20, 5);

            //Act: Arrange:
            converter.LoadResources(new Wood(), 5);
            Assert.AreEqual(5, converter.LoadZone.GetResourcesCount());
            Assert.AreEqual(0, converter.UnloadZone.GetResourcesCount());

            converter.Update(3);
            converter.Update(3);
            converter.Update(3);
            Assert.IsFalse(converter.IsWorking);
            Assert.AreEqual(expectedInputCount, converter.LoadZone.GetResourcesCount());
            Assert.AreEqual(expectedOutputCount, converter.UnloadZone.GetResourcesCount());
        }

        [Test]
        public void UpdateConverterFinishingConvertManyResources()
        {
            //Arrange:
            var recipe = new Recipe(new Wood(), 1, new Board(), 1);
            var converter = new Converter(recipe, 1, 5, 5, 1);

            //Act: Arrange:
            converter.LoadResources(new Wood(), 5);
            for (int i = 0, j = 5; i < j; i++)
            {
                Assert.AreEqual(converter.LoadZone.GetResourcesCount(), j - i);
                Assert.AreEqual(converter.UnloadZone.GetResourcesCount(), i);

                converter.Update(1);
            }

            Assert.IsFalse(converter.IsWorking);
        }


        //Turn off Converter
        [Test]
        public void TurnOffConverter()
        {
            //Arrange:
            var recipe = new Recipe(new Wood(), 1, new Board(), 1);
            var converter = new Converter(recipe, 3, 5, 5, 3);

            //Act:
            converter.TurnOff();

            //Assert:
            Assert.IsFalse(converter.IsWorking);
        }

        [Test]
        public void TurnOffConverterWhenWorking()
        {
            //Arrange:
            var recipe = new Recipe(new Wood(), 1, new Board(), 1);
            var converter = new Converter(recipe, 3, 5, 5, 3);

            //Act: Arrange:
            converter.LoadResources(new Wood(), 1);
            converter.Update(1);
            Assert.IsTrue(converter.IsWorking);

            converter.TurnOff();
            Assert.IsFalse(converter.IsWorking);
        }

        [Test]
        public void TurnOffConverterWhenWorkingWithReturning()
        {
            //Arrange:
            var recipe = new Recipe(new Wood(), 1, new Board(), 1);
            var converter = new Converter(recipe, 3, 5, 5, 3);

            //Act: Arrange:
            converter.LoadResources(new Wood(), 10);
            Assert.AreEqual(5, converter.LoadZone.GetResourcesCount());

            converter.Update(1);
            Assert.AreEqual(2, converter.LoadZone.GetResourcesCount());

            converter.TurnOff();
            Assert.AreEqual(5, converter.LoadZone.GetResourcesCount());
        }


        [Test]
        public void TurnOffConverterWhenWorkingWithReturningAndBurning()
        {
            //Arrange:
            var recipe = new Recipe(new Wood(), 1, new Board(), 1);
            var converter = new Converter(recipe, 3, 5, 5, 3);

            //Act: Arrange:
            converter.LoadResources(new Wood(), 10);
            Assert.AreEqual(converter.LoadZone.GetResourcesCount(), 5);

            converter.Update(1);
            converter.LoadResources(new Wood(), 10);
            Assert.AreEqual(converter.LoadZone.GetResourcesCount(), 5);

            converter.TurnOff();
            Assert.AreEqual(converter.LoadZone.GetResourcesCount(), 5);
        }
    }
}