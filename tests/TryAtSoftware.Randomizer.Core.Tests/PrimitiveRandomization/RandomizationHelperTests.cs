namespace TryAtSoftware.Randomizer.Core.Tests.PrimitiveRandomization;

using System;
using TryAtSoftware.Randomizer.Core.Helpers;
using Xunit;

public class RandomizationHelperTests
{
    [Fact]
    public void GetRandomStringShouldValidateLength()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => RandomizationHelper.GetRandomString(-1, "mask"));
    }

    [Theory]
    [MemberData(nameof(TestsHelper.GetInvalidStringParameters), MemberType = typeof(TestsHelper))]
    public void GetRandomStringShouldValidateMask(string mask)
    {
        Assert.Throws<ArgumentNullException>(() => RandomizationHelper.GetRandomString(5, mask));
    }

    [Fact]
    public void RandomIntegerShouldNeverExceedBoundaries()
    {
        const int iterations = 10000;
        for (var i = 1; i <= iterations; i++)
        {
            var randomInteger = RandomizationHelper.RandomInteger(0, i);

            Assert.True(randomInteger >= 0);
            Assert.True(randomInteger < i);
        }

        for (var i = 1; i <= iterations; i++)
        {
            var randomInteger = RandomizationHelper.RandomInteger(0, i, upperBoundIsExclusive: false);

            Assert.True(randomInteger >= 0);
            Assert.True(randomInteger <= i);
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