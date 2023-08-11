namespace TryAtSoftware.Randomizer.Core.Tests.PrimitiveRandomization;

using System;
using System.Collections.Generic;
using TryAtSoftware.Randomizer.Core.Helpers;
using Xunit;

public class RandomizationHelperTests
{
    private const int ITERATIONS = 500;
    
    [Fact]
    public void GetRandomStringShouldValidateLength()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => RandomizationHelper.GetRandomString(-1, "mask"));
    }

    [Fact]
    public void GetRandomStringShouldValidateMask()
    {
        Assert.Throws<ArgumentNullException>(() => RandomizationHelper.GetRandomString(5, null!));
        Assert.Throws<ArgumentNullException>(() => RandomizationHelper.GetRandomString(5, string.Empty));
    }

    [Fact]
    public void RandomIntegerShouldBeGeneratedCorrectly()
    {
        for (var i = 1; i <= ITERATIONS; i++)
        {
            var randomNumber = RandomizationHelper.RandomInteger(0, i);

            Assert.True(randomNumber >= 0);
            Assert.True(randomNumber < i);
        }

        for (var i = 1; i <= ITERATIONS; i++)
        {
            var randomNumber = RandomizationHelper.RandomInteger(0, i, upperBoundIsExclusive: false);

            Assert.True(randomNumber >= 0);
            Assert.True(randomNumber <= i);
        }

        var seen = new HashSet<int>();
        for (var i = 0; i < ITERATIONS; i++)
        {
            var randomNumber = RandomizationHelper.RandomInteger();
            Assert.DoesNotContain(randomNumber, seen);
            seen.Add(randomNumber);
        }
    }

    [Fact]
    public void RandomUnsignedIntegerShouldBeGeneratedCorrectly()
    {
        for (var i = 1U; i <= ITERATIONS; i++)
        {
            uint inclusiveLowerBound = 1000U, exclusiveUpperBound = 1000U + i;
            var randomNumber = RandomizationHelper.RandomUnsignedInteger(inclusiveLowerBound, exclusiveUpperBound);

            Assert.True(randomNumber >= inclusiveLowerBound);
            Assert.True(randomNumber < exclusiveUpperBound);
        }

        for (var i = 1U; i <= ITERATIONS; i++)
        {
            uint inclusiveLowerBound = 1000U, inclusiveUpperBound = 1000U + i;
            var randomNumber = RandomizationHelper.RandomUnsignedInteger(inclusiveLowerBound, inclusiveUpperBound, upperBoundIsExclusive: false);

            Assert.True(randomNumber >= inclusiveLowerBound);
            Assert.True(randomNumber <= inclusiveUpperBound);
        }

        var seen = new HashSet<uint>();
        for (var i = 0; i < ITERATIONS; i++)
        {
            var randomNumber = RandomizationHelper.RandomUnsignedInteger();
            Assert.DoesNotContain(randomNumber, seen);
            seen.Add(randomNumber);
        }
    }

    [Fact]
    public void RandomLongIntegerShouldBeGeneratedCorrectly()
    {
        for (var i = 1; i <= ITERATIONS; i++)
        {
            var randomNumber = RandomizationHelper.RandomLongInteger(0, i);

            Assert.True(randomNumber >= 0);
            Assert.True(randomNumber < i);
        }

        for (var i = 1; i <= ITERATIONS; i++)
        {
            var randomNumber = RandomizationHelper.RandomLongInteger(0, i, upperBoundIsExclusive: false);

            Assert.True(randomNumber >= 0);
            Assert.True(randomNumber <= i);
        }

        var seen = new HashSet<long>();
        for (var i = 0; i < ITERATIONS; i++)
        {
            var randomNumber = RandomizationHelper.RandomLongInteger();
            Assert.DoesNotContain(randomNumber, seen);
            seen.Add(randomNumber);
        }
    }

    [Fact]
    public void RandomUnsignedLongIntegerShouldBeGeneratedCorrectly()
    {
        for (var i = 1UL; i <= ITERATIONS; i++)
        {
            ulong inclusiveLowerBound = 1000UL, exclusiveUpperBound = 1000U + i;
            var randomNumber = RandomizationHelper.RandomUnsignedLongInteger(inclusiveLowerBound, exclusiveUpperBound);

            Assert.True(randomNumber >= inclusiveLowerBound);
            Assert.True(randomNumber < exclusiveUpperBound);
        }

        for (var i = 1UL; i <= ITERATIONS; i++)
        {
            ulong inclusiveLowerBound = 1000UL, inclusiveUpperBound = 1000U + i;
            var randomNumber = RandomizationHelper.RandomUnsignedLongInteger(inclusiveLowerBound, inclusiveUpperBound, upperBoundIsExclusive: false);

            Assert.True(randomNumber >= inclusiveLowerBound);
            Assert.True(randomNumber <= inclusiveUpperBound);
        }

        var seen = new HashSet<ulong>();
        for (var i = 0; i < ITERATIONS; i++)
        {
            var randomNumber = RandomizationHelper.RandomUnsignedLongInteger();
            Assert.DoesNotContain(randomNumber, seen);
            seen.Add(randomNumber);
        }
    }

    [Fact]
    public void RandomDoubleShouldWorkCorrectly()
    {
        var seen = new HashSet<double>();
        for (var i = 0; i < ITERATIONS; i++)
        {
            var randomNumber = RandomizationHelper.RandomDouble();
            
            Assert.True(randomNumber >= 0);
            Assert.True(randomNumber < 1);

            Assert.DoesNotContain(randomNumber, seen);
            seen.Add(randomNumber);
        }
    }

    [Fact]
    public void RandomFloatShouldWorkCorrectly()
    {
        var seen = new HashSet<float>();
        for (var i = 0; i < ITERATIONS; i++)
        {
            var randomNumber = RandomizationHelper.RandomFloat();
            
            Assert.True(randomNumber >= 0);
            Assert.True(randomNumber < 1);

            Assert.DoesNotContain(randomNumber, seen);
            seen.Add(randomNumber);
        }
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(3, 1)]
    public void RandomIntegerGenerationWithInvalidBoundariesShouldFail(int min, int max)
    {
        Assert.Throws<InvalidOperationException>(() => _ = RandomizationHelper.RandomInteger(min, max));
    }
}