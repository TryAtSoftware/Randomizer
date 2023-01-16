namespace TryAtSoftware.Randomizer.Core.Tests.PrimitiveRandomization;

using TryAtSoftware.Randomizer.Core.Primitives;
using Xunit;

public class RandomizerConverterTests
{
    [Fact]
    public void NullValuesShouldBeConvertedToObjectSuccessfully()
    {
        var constantNullRandomizer = ((string?)null).AsConstantRandomizer();
        var randomizerConverter = new RandomizerConverter<string?, object?>(constantNullRandomizer);
        var randomValue = randomizerConverter.PrepareRandomValue();
        Assert.Null(randomValue);
    }
}