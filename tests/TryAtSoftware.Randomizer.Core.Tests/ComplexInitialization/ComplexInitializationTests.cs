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
        complexRandomizer.Randomize(p => p.Id, new GuidRandomizer());
        complexRandomizer.Randomize(p => p.Name, new StringRandomizer());
        complexRandomizer.Randomize(p => p.Age, new NumberRandomizer());
        complexRandomizer.Randomize(p => p.IsEmployed, new BooleanRandomizer());
        complexRandomizer.Randomize(p => p.EventDate, new DateTimeOffsetRandomizer());

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
        complexRandomizer.Randomize(x => x.Make, new StringRandomizer());
        complexRandomizer.Randomize(x => x.Model, new StringRandomizer());
        complexRandomizer.Randomize(x => x.Year, new NumberRandomizer());

        var firstCar = complexRandomizer.PrepareRandomValue();
        Assert.NotNull(firstCar);

        var secondCar = complexRandomizer.PrepareRandomValue();
        Assert.NotNull(secondCar);
        Assert.NotSame(firstCar, secondCar);
    }

    [Fact]
    public void OrderOfRandomizationRulesShouldBeAccepted()
    {
        var randomizationRuleBuilders = new List<Func<IRandomValueSetter<Person>, IRandomizationRule<Person>>>(capacity: 5)
        {
            vs => new RandomizationRule<Person, Guid>(x => x.Id, vs),
            vs => new RandomizationRule<Person, string>(x => x.Name, vs),
            vs => new RandomizationRule<Person, int>(x => x.Age, vs),
            vs => new RandomizationRule<Person, DateTimeOffset>(x => x.EventDate, vs),
            vs => new RandomizationRule<Person, bool>(x => x.IsEmployed, vs)
        };

        var invocationOrder = new List<int>();
        var valueSetterMocks = new Mock<IRandomValueSetter<Person>>[randomizationRuleBuilders.Count];
        var valueSetters = new IRandomValueSetter<Person>[randomizationRuleBuilders.Count];

        for (var i = 0; i < randomizationRuleBuilders.Count; i++)
        {
            var randomValueSetterMock = new Mock<IRandomValueSetter<Person>>();
            
            var valueSetterIndex = i;
            randomValueSetterMock.Setup(x => x.SetValue(It.IsAny<Person>())).Callback(() => invocationOrder.Add(valueSetterIndex));

            valueSetterMocks[i] = randomValueSetterMock;
            valueSetters[i] = randomValueSetterMock.Object;
        }

        for (var i = 0; i < 10; i++)
        {
            var complexRandomizer = new ComplexRandomizer<Person>();
            var valueSetterIndices = Enumerable.Range(0, randomizationRuleBuilders.Count).ToArray();
            TestsHelper.Shuffle(valueSetterIndices);

            for (var j = 0; j < randomizationRuleBuilders.Count; j++)
            {
                var rule = randomizationRuleBuilders[j].Invoke(valueSetters[valueSetterIndices[j]]);
                complexRandomizer.Randomize(rule);
            }

            var person = complexRandomizer.PrepareRandomValue();
            Assert.NotNull(person);
            
            Assert.Equal(randomizationRuleBuilders.Count, invocationOrder.Count);
            for (var j = 0; j < randomizationRuleBuilders.Count; j++) Assert.Equal(valueSetterIndices[j], invocationOrder[j]);
            for (var j = 0; j < randomizationRuleBuilders.Count; j++) valueSetterMocks[j].Verify(x => x.SetValue(person));
            
            invocationOrder.Clear();
        }
        
        for (var i = 0; i < randomizationRuleBuilders.Count; i++) valueSetterMocks[i].VerifyNoOtherCalls();
    }
}
