namespace TryAtSoftware.Randomizer.Core.Tests.PrimitiveRandomization;

using System;
using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Randomizer.Core.Helpers;
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
                _ = new CollectionRandomizer<int>(null!);
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

    [Fact]
    public void RandomizingCollectionsWithConstantLengthShouldBeSupported()
    {
        var randomLength = RandomizationHelper.RandomInteger(3, 5);

        var textRandomizer = new StringRandomizer();
        var collectionRandomizer = new CollectionRandomizer<string>(textRandomizer, randomLength, randomLength);

        var randomCollection = collectionRandomizer.PrepareRandomValue();
        Assert.NotNull(randomCollection);

        var iteratedRandomCollection = randomCollection.ToArray();
        Assert.Equal(randomLength, iteratedRandomCollection.Length);
    }
}