namespace TryAtSoftware.Randomizer.Core.Tests;

using System;
using TryAtSoftware.Randomizer.Core.Primitives;
using TryAtSoftware.Randomizer.Core.Tests.Models;
using Xunit;

public class ComplexRandomizerTests
{
    [Fact]
    public void RandomizeShouldThrowExceptionWhenInvalidParametersAreProvided()
    {
        var randomizer = new ComplexRandomizer<Person>();
        Assert.Throws<ArgumentNullException>(() => randomizer.Randomize(null!));
        Assert.Throws<ArgumentNullException>(() => randomizer.Randomize(null!, new StringRandomizer()));
        Assert.Throws<ArgumentNullException>(() => randomizer.Randomize<string>(x => x.Name, randomizer: null!));
        Assert.Throws<ArgumentNullException>(() => randomizer.Randomize(null!, _ => new StringRandomizer()));
        Assert.Throws<ArgumentNullException>(() => randomizer.Randomize<string>(x => x.Name, getRandomizer: null!));
    }

    [Fact]
    public void RandomizationRulesShouldBeAddedSuccessfully()
    {
        var randomizer = new ComplexRandomizer<Person>();
        randomizer.Randomize(x => x.Id, new GuidRandomizer());
        randomizer.Randomize(x => x.Name, _ => new StringRandomizer());
        randomizer.Randomize(new RandomizationRule<Person, int>(x => x.Age, new NumberRandomizer(min: 10, max: 50)));

        var person = randomizer.PrepareRandomValue();
        Assert.NotEqual(Guid.Empty, person.Id);
        Assert.False(string.IsNullOrWhiteSpace(person.Name));
        Assert.NotEqual(0, person.Age);
    }

    [Fact]
    public void RandomizationRulesShouldBeOverriddenSuccessfully()
    {
        var randomizer = new ComplexRandomizer<Person>();
        randomizer.Randomize(x => x.Age, new NumberRandomizer(min: 10, max: 20));
        randomizer.Randomize(x => x.Age, new NumberRandomizer(min: 100, max: 200));

        var person = randomizer.PrepareRandomValue();
        Assert.True(100 <= person.Age && person.Age < 200);
    }
}