using System;
using NUnit.Framework;

namespace Converter.Tests
{
    public class RecipeTests
    {
        [Test]
        public void RecipeCreate()
        {
            var recipe = new Recipe(new Wood(), 1, new Board(), 1);
            Assert.IsNotNull(recipe);
        }

        [Test]
        public void RecipeCreateWithNullResourceThenThrowException()
        {
            Assert.Catch<ArgumentNullException>(() =>
                new Recipe(null, 1, new Board(), 1));
            Assert.Catch<ArgumentNullException>(() =>
                new Recipe(new Wood(), 1, null, 1));
            Assert.Catch<ArgumentNullException>(() =>
                new Recipe(null, 1, null, 1));
        }

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        [TestCase(0, 0)]
        [TestCase(-1, -1)]
        public void RecipeCreateWithInvalidCountThenThrowException(int inputCount, int outputCount)
        {
            Assert.Catch<ArgumentOutOfRangeException>(
                () => new Recipe(new Wood(), inputCount, new Board(), outputCount));
        }
    }
}