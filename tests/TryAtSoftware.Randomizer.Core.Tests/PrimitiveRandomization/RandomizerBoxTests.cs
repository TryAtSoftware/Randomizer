namespace TryAtSoftware.Randomizer.Core.Tests.PrimitiveRandomization;

using System;
using TryAtSoftware.Randomizer.Core.Primitives;
using Xunit;

public class RandomizerBoxTests
{
    [Fact]
    public void RandomizerBoxShouldNotBeInstantiatedSuccessfullyWithInvalidConstructorParameters() => Assert.Throws<ArgumentNullException>(() => new RandomizerBox<int>(null!));
}