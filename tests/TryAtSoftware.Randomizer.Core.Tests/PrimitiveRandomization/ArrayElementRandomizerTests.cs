namespace TryAtSoftware.Randomizer.Core.Tests.PrimitiveRandomization;

using System;
using System.Collections.Generic;
using TryAtSoftware.Randomizer.Core.Primitives;
using Xunit;

public class ArrayElementRandomizerTests
{
    [Fact]
    public void InitializingArrayElementRandomizerWithNullShouldResultInException()
    {
        Assert.Throws<ArgumentNullException>(() => new ArrayElementRandomizer<int>(null!));
    }
        
    [Fact]
    public void RandomizationOfEmptyArrayShouldNotBeSupported()
    {
        var emptyArray = Array.Empty<int>();
        Assert.Throws<ArgumentException>(() => new ArrayElementRandomizer<int>(emptyArray));
    }

    [Fact]
    public void RandomizationOfAnyArrayShouldReturnTheElementOfAnyRandomIndex()
    {
        var testArray = new int[15];
        for (var i = 0; i < testArray.Length; i++)
            testArray[i] = i + 1;

        var arrayElementRandomizer = new ArrayElementRandomizer<int>(testArray);
        var randomElementsHashSet = new HashSet<int>();
        for (var i = 0; i < 100; i++)
        {
            var randomElement = arrayElementRandomizer.PrepareRandomValue();
            randomElementsHashSet.Add(randomElement);
                
            Assert.True(randomElement >= 1);
            Assert.True(randomElement <= 15);
        }
            
        Assert.True(randomElementsHashSet.Count >= 5);
    }
}