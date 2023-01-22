namespace TryAtSoftware.Randomizer.Core.Tests;

using System;
using TryAtSoftware.Randomizer.Core.Helpers;
using TryAtSoftware.Randomizer.Core.Interfaces;
using TryAtSoftware.Randomizer.Core.Primitives;
using TryAtSoftware.Randomizer.Core.Tests.Models;
using Xunit;

public static class RandomizationRuleTests
{
    [Fact]
    public static void RandomizationRuleShouldBeInstantiatedCorrectly()
    {
        Assert.Throws<ArgumentNullException>(() => new RandomizationRule<Person, string>(null!, valueSetter: null!));
        Assert.Throws<ArgumentNullException>(() => new RandomizationRule<Person, string>(null!, getRandomizer: null!));
        Assert.Throws<ArgumentNullException>(() => new RandomizationRule<Person, string>(null!, randomizer: null!));
        Assert.Throws<ArgumentNullException>(() => new RandomizationRule<Person, string>(p => p.Name, valueSetter: null!));
        Assert.Throws<ArgumentNullException>(() => new RandomizationRule<Person, string>(p => p.Name, randomizer: null!));
        Assert.Throws<ArgumentNullException>(() => new RandomizationRule<Person, string>(p => p.Name, valueSetter: null!, new StringRandomizer()));
        Assert.Throws<ArgumentNullException>(() => new RandomizationRule<Person, string>(p => p.Name, getRandomizer: null!, new StringRandomizer()));
    }

    [Fact]
    public static void RandomizationRuleShouldBeSuccessfullyInstantiatedWhenUsingRandomizerResolvingFunction()
    {
        var randomizationRule = new RandomizationRule<Person, string>(x => x.Name, _ => new StringRandomizer());

        var valueSetter = randomizationRule.GetValueSetter();
        var parameterRandomizer = randomizationRule.GetParameterRandomizer();
        
        var repetitionCount = RandomizationHelper.RandomInteger(10, 100);
        for (var i = 0; i < repetitionCount; i++)
        {
            Assert.Same(valueSetter, randomizationRule.GetValueSetter());
            Assert.Same(parameterRandomizer, randomizationRule.GetParameterRandomizer());
        }
    }
}