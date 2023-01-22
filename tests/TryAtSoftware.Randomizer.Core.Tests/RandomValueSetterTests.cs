namespace TryAtSoftware.Randomizer.Core.Tests;

using System;
using TryAtSoftware.Randomizer.Core.Helpers;
using TryAtSoftware.Randomizer.Core.Primitives;
using TryAtSoftware.Randomizer.Core.Tests.Models;
using Xunit;

public class RandomValueSetterTests
{
    [Fact]
    public void RandomValueSetterShouldWorkNotSetAnyValueIfTheResolvedRandomizerIsNull()
    {
        var originalId = Guid.NewGuid();
        var instance = new Person { Id = originalId };

        var randomValueSetter = new RandomValueSetter<Person, Guid>(nameof(Person.Id), _ => null, ModelInfo<Person>.Instance);
        randomValueSetter.SetValue(instance);

        Assert.Equal(originalId, instance.Id);
    }

    [Fact]
    public void RandomValueSetterShouldCallTheRandomizerResolvingFunctionEveryTime()
    {
        var invocationCount = 0;
        var randomValueSetter = new RandomValueSetter<Person, Guid>(
            nameof(Person.Id),
            _ =>
            {
                invocationCount++;
                return new GuidRandomizer();
            },
            ModelInfo<Person>.Instance);

        var repetitionCount = RandomizationHelper.RandomInteger(10, 100);
        var instance = new Person();
        
        for (var i = 0; i < repetitionCount; i++)
        {
            randomValueSetter.SetValue(instance);
            Assert.Equal(i + 1, invocationCount);
        }
    }
}