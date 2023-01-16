namespace TryAtSoftware.Randomizer.Core.Tests;

using System;
using TryAtSoftware.Randomizer.Core.Primitives;
using TryAtSoftware.Randomizer.Core.Tests.Models;
using Xunit;

public static class RandomizationRuleTests
{
    [Fact]
    public static void RandomizationRuleShouldBeInstantiatedCorrectly()
    {
        Assert.Throws<ArgumentNullException>(() => new RandomizationRule<Person, string>(null!, valueSetter: null!));
        Assert.Throws<ArgumentNullException>(() => new RandomizationRule<Person, string>(null!, randomizer: null!));
        Assert.Throws<ArgumentNullException>(() => new RandomizationRule<Person, string>(null!, null!, null));
        Assert.Throws<ArgumentNullException>(() => new RandomizationRule<Person, string>(p => p.Name, valueSetter: null!));
        Assert.Throws<ArgumentNullException>(() => new RandomizationRule<Person, string>(p => p.Name, randomizer: null!));
        Assert.Throws<ArgumentNullException>(() => new RandomizationRule<Person, string>(p => p.Name, null!, null));
        Assert.Throws<ArgumentNullException>(() => new RandomizationRule<Person, string>(p => p.Name, null!, new StringRandomizer()));
    }
}