namespace TryAtSoftware.Randomizer.Core.Tests.PrimitiveRandomization;

using System;
using TryAtSoftware.Randomizer.Core.Primitives;
using Xunit;

public class RandomizerSelectorTests
{
    [Fact]
    public void InitializingRandomizerSelectorWithNullShouldResultInException()
    {
        Assert.Throws<ArgumentNullException>(
            () =>
            {
                _ = new RandomizerSelector<int>(null!);
            });
    }
}