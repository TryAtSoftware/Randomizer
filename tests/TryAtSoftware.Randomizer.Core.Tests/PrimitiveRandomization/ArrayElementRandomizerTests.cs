namespace TryAtSoftware.Randomizer.Core.Tests.PrimitiveRandomization
{
    using System;
    using System.Collections.Generic;
    using TryAtSoftware.Randomizer.Core.Primitives;
    using Xunit;

    public class ArrayElementRandomizerTests
    {
        [Fact]
        public void InitializingArrayElementRandomizerWithNullShouldResultInException()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    _ = new ArrayElementRandomizer<int>(null);
                });
        }
        
        [Fact]
        public void RandomizationOfEmptyArrayShouldResultInDefault()
        {
            var emptyArray = Array.Empty<int>();
            var arrayElementRandomizer = new ArrayElementRandomizer<int>(emptyArray);

            for (var i = 0; i < 100; i++)
            {
                var randomElement = arrayElementRandomizer.PrepareRandomValue();
                Assert.Equal(default, randomElement);
            }
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
}