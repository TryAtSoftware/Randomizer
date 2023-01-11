namespace TryAtSoftware.Randomizer.Core.Tests.ComplexInitialization;

using TryAtSoftware.Randomizer.Core.Primitives;
using TryAtSoftware.Randomizer.Core.Tests.InstanceBuilders;
using TryAtSoftware.Randomizer.Core.Tests.Models;
using Xunit;

public class ComplexInitializationTests
{
    [Fact]
    public void ComplexRandomizerShouldWorkCorrectly()
    {
        var instanceBuilder = new PersonInstanceBuilder();
        var complexRandomizer = new ComplexRandomizer<Person>(instanceBuilder);
        complexRandomizer.AddRandomizationRule(p => p.Id, new GuidRandomizer());
        complexRandomizer.AddRandomizationRule(p => p.Name, new StringRandomizer());
        complexRandomizer.AddRandomizationRule(p => p.Age, new NumberRandomizer());
        complexRandomizer.AddRandomizationRule(p => p.IsEmployed, new BooleanRandomizer());
        complexRandomizer.AddRandomizationRule(p => p.EventDate, new DateTimeOffsetRandomizer());

        var firstPerson = complexRandomizer.PrepareRandomValue();
        Assert.NotNull(firstPerson);

        var secondPerson = complexRandomizer.PrepareRandomValue();
        Assert.NotSame(firstPerson, secondPerson);
        Assert.NotEqual(firstPerson, secondPerson, new PeopleComparer());
    }

    [Fact]
    public void ComplexInstanceBuildingProcessShouldBeExecutedCorrectly()
    {
        var complexRandomizer = new ComplexRandomizer<Car>();
        complexRandomizer.AddRandomizationRule(x => x.Make, new StringRandomizer());
        complexRandomizer.AddRandomizationRule(x => x.Model, new StringRandomizer());
        complexRandomizer.AddRandomizationRule(x => x.Year, new NumberRandomizer());

        var firstCar = complexRandomizer.PrepareRandomValue();
        Assert.NotNull(firstCar);

        var secondCar = complexRandomizer.PrepareRandomValue();
        Assert.NotNull(secondCar);
        Assert.NotSame(firstCar, secondCar);
    }
}
