namespace TryAtSoftware.Randomizer.Core.Tests.PrimitiveRandomization
{
    using System;
    using System.Collections.Generic;
    using TryAtSoftware.Randomizer.Core.Primitives;
    using Xunit;

    public class CollectionRandomizerTests
    {
        [Fact]
        public void InitializingCollectionRandomizerWithNullShouldResultInException()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    _ = new CollectionRandomizer<int>(null);
                });
        }

        [Fact]
        public void RandomizedCollectionShouldNotBeEmpty()
        {
            var numberRandomizer = new NumberRandomizer();
            var collectionRandomizer = new CollectionRandomizer<int>(numberRandomizer, minLength: 10, maxLength: 100);

            var randomCollection = collectionRandomizer.PrepareRandomValue();
            Assert.NotNull(randomCollection);

            var valuesHashSet = new HashSet<int>();
            foreach (var element in randomCollection)
                valuesHashSet.Add(element);

            Assert.True(valuesHashSet.Count >= 5);
        }
    }
}