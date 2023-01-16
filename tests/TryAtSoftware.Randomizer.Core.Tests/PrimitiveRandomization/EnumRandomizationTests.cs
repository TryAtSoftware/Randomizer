namespace TryAtSoftware.Randomizer.Core.Tests.PrimitiveRandomization;

using System.Collections.Generic;
using TryAtSoftware.Randomizer.Core.Primitives;
using TryAtSoftware.Randomizer.Core.Tests.Models;
using Xunit;

public class EnumRandomizationTests
{
    [Fact]
    public void EnumRandomizationShouldProduceDifferentValues()
    {
        var enumRandomizer = new EnumRandomizer<Priority>();

        var values = new HashSet<Priority>();
        for (var i = 0; i < 100; i++)
            values.Add(enumRandomizer.PrepareRandomValue());
            
        Assert.True(values.Count > 1);
    }
}