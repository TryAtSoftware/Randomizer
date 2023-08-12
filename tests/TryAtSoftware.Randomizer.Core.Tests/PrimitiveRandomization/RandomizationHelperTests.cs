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
        Assert.Throws<ArgumentOutOfRangeException>(() => RandomizationHelper.GetRandomString(-1, new[] { 'a' }));
    }

    [Fact]
    public void GetRandomStringShouldValidateMask()
    {
        Assert.Throws<ArgumentNullException>(() => RandomizationHelper.GetRandomString(5, (string)null!));
        Assert.Throws<ArgumentNullException>(() => RandomizationHelper.GetRandomString(5, string.Empty));

        Assert.Throws<ArgumentNullException>(() => RandomizationHelper.GetRandomString(5, (char[])null!));
        Assert.Throws<ArgumentException>(() => RandomizationHelper.GetRandomString(5, Array.Empty<char>()));
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

        var seen = new Dictionary<int, int>();
        for (var i = 0; i < ITERATIONS; i++)
        {
            var randomNumber = RandomizationHelper.RandomInteger();
            
            seen.TryAdd(randomNumber, 0);
            seen[randomNumber]++;
            
            Assert.True(seen[randomNumber] <= 2);
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

        var seen = new Dictionary<uint, int>();
        for (var i = 0; i < ITERATIONS; i++)
        {
            var randomNumber = RandomizationHelper.RandomUnsignedInteger();
            
            seen.TryAdd(randomNumber, 0);
            seen[randomNumber]++;
            
            Assert.True(seen[randomNumber] <= 2);
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

        var seen = new Dictionary<long, int>();
        for (var i = 0; i < ITERATIONS; i++)
        {
            var randomNumber = RandomizationHelper.RandomLongInteger();
            
            seen.TryAdd(randomNumber, 0);
            seen[randomNumber]++;
            
            Assert.True(seen[randomNumber] <= 2);
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

        var seen = new Dictionary<ulong, int>();
        for (var i = 0; i < ITERATIONS; i++)
        {
            var randomNumber = RandomizationHelper.RandomUnsignedLongInteger();
            
            seen.TryAdd(randomNumber, 0);
            seen[randomNumber]++;
            
            Assert.True(seen[randomNumber] <= 2);
        }
    }

    [Fact]
    public void RandomDoubleShouldBeGeneratedCorrectly()
    {
        var seen = new Dictionary<double, int>();
        for (var i = 0; i < ITERATIONS; i++)
        {
            var randomNumber = RandomizationHelper.RandomDouble();

            Assert.True(randomNumber >= 0);
            Assert.True(randomNumber < 1);
            
            seen.TryAdd(randomNumber, 0);
            seen[randomNumber]++;
            
            Assert.True(seen[randomNumber] <= 2);
        }
    }

    [Fact]
    public void RandomFloatShouldBeGeneratedCorrectly()
    {
        var seen = new Dictionary<float, int>();
        for (var i = 0; i < ITERATIONS; i++)
        {
            var randomNumber = RandomizationHelper.RandomFloat();

            Assert.True(randomNumber >= 0);
            Assert.True(randomNumber < 1);
            
            seen.TryAdd(randomNumber, 0);
            seen[randomNumber]++;
            
            Assert.True(seen[randomNumber] <= 2);
        }
    }

    [Fact]
    public void RandomDateTimeOffsetShouldBeGeneratedCorrectly()
    {
        var seen = new Dictionary<DateTimeOffset, int>();
        for (var i = 0; i < ITERATIONS; i++)
        {
            var randomPastDateTime = RandomizationHelper.GetRandomDateTimeOffset(historical: true);
            Assert.True(randomPastDateTime < DateTimeOffset.Now);

            seen.TryAdd(randomPastDateTime, 0);
            seen[randomPastDateTime]++;
            
            Assert.True(seen[randomPastDateTime] <= 2);

            var randomFutureDateTime = RandomizationHelper.GetRandomDateTimeOffset(historical: false);
            Assert.True(randomFutureDateTime > DateTimeOffset.Now);

            seen.TryAdd(randomFutureDateTime, 0);
            seen[randomFutureDateTime]++;
            
            Assert.True(seen[randomFutureDateTime] <= 2);
        }
    }

    [Theory]
    [InlineData(1UL, 1UL, true)]
    [InlineData(1UL, 0UL, true)]
    [InlineData(1UL, 0UL, false)]
    public void RandomIntegerGenerationWithInvalidBoundariesShouldFail(ulong min, ulong max, bool upperBoundIsExclusive)
    {
        Assert.Throws<InvalidOperationException>(() => _ = RandomizationHelper.RandomUnsignedLongInteger(min, max, upperBoundIsExclusive));
    }
}