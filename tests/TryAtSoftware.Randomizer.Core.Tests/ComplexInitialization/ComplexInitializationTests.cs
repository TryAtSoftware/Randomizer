namespace TryAtSoftware.Randomizer.Core.Tests.ComplexInitialization;

using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using TryAtSoftware.Randomizer.Core.Interfaces;
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

    [Fact]
    public void OrderOfRandomizationRulesShouldBeAccepted()
    {
        var invocationOrder = new List<int>();
        var valueSetters = new IRandomValueSetter<Car>[3];

        for (var i = 0; i < 3; i++)
        {
            var valueSetterIndex = i;
            var randomValueSetterMock = new Mock<IRandomValueSetter<Car>>();
            randomValueSetterMock.Setup(x => x.SetValue(It.IsAny<Car>())).Callback(() => invocationOrder.Add(valueSetterIndex));
            valueSetters[i] = randomValueSetterMock.Object;
        }

        var repetitionCount = 5;
        for (var i = 0; i < repetitionCount; i++)
        {
            var complexRandomizer = new ComplexRandomizer<Car>();
            var valueSetterIndices = Enumerable.Range(0, 3);
                invocationOrder.Clear();
        }
    }
}
