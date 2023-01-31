namespace TryAtSoftware.Randomizer.Core.Tests;

using System.Collections.Generic;
using TryAtSoftware.Randomizer.Core.Interfaces;
using TryAtSoftware.Randomizer.Core.Primitives;
using Xunit;

public static class GeneralInstanceBuilderTests
{
    [Fact]
    public static void GeneralInstanceBuilderShouldWorkCorrectlyByDefault()
    {
        var instanceBuilder = new GeneralInstanceBuilder<SimpleEntity>();
        var instanceBuildingResult = instanceBuilder.PrepareNewInstance(PrepareInstanceBuildingArguments());
        Assert.NotNull(instanceBuildingResult);
        Assert.NotNull(instanceBuildingResult.Instance);
        Assert.Equal(default, instanceBuildingResult.Instance.Text);
        Assert.Equal(default, instanceBuildingResult.Instance.Number);
        Assert.False(instanceBuildingResult.IsUsed(nameof(SimpleEntity.Text)));
        Assert.False(instanceBuildingResult.IsUsed(nameof(SimpleEntity.Number)));
    }

    [Fact]
    public static void GeneralInstanceBuilderShouldWorkCorrectlyWithAnyConstructorIfSuitableParametersAreProvided()
    {
        var instanceBuilder = new GeneralInstanceBuilder<MoreComplexEntity>();
        var instanceBuildingResult = instanceBuilder.PrepareNewInstance(PrepareInstanceBuildingArguments());
        Assert.NotNull(instanceBuildingResult);
        Assert.NotNull(instanceBuildingResult.Instance);
        Assert.NotEqual(default, instanceBuildingResult.Instance.Text);
        Assert.Equal(default, instanceBuildingResult.Instance.Number);
        Assert.True(instanceBuildingResult.IsUsed(nameof(SimpleEntity.Text)));
        Assert.False(instanceBuildingResult.IsUsed(nameof(SimpleEntity.Number)));
    }

    [Fact]
    public static void GeneralInstanceBuilderShouldAlwaysChoseTheMostSuitableConstructor()
    {
        var instanceBuilder = new GeneralInstanceBuilder<MostComplexEntity>();
        var instanceBuildingResult = instanceBuilder.PrepareNewInstance(PrepareInstanceBuildingArguments());
        Assert.NotNull(instanceBuildingResult);
        Assert.NotNull(instanceBuildingResult.Instance);
        Assert.NotEqual(default, instanceBuildingResult.Instance.Text);
        Assert.NotEqual(default, instanceBuildingResult.Instance.Number);
        Assert.True(instanceBuildingResult.IsUsed(nameof(SimpleEntity.Text)));
        Assert.True(instanceBuildingResult.IsUsed(nameof(SimpleEntity.Number)));
    }

    [Fact]
    public static void GeneralInstanceBuilderShouldNotBeAbleToInstantiateGivenTypeWithoutEnoughParametersForTheConstructor()
    {
        var instanceBuilder = new GeneralInstanceBuilder<TooComplexEntity>();

        var instanceBuildingResult = instanceBuilder.PrepareNewInstance(PrepareInstanceBuildingArguments());
        Assert.NotNull(instanceBuildingResult);
        Assert.Null(instanceBuildingResult.Instance);
    }

    private static IInstanceBuildingArguments PrepareInstanceBuildingArguments()
    {
        var randomizers = new Dictionary<string, IRandomizer<object?>>(capacity: 2);
        randomizers[nameof(SimpleEntity.Text)] = new RandomizerBox<string>(new StringRandomizer());
        randomizers[nameof(SimpleEntity.Number)] = new RandomizerConverter<int, object>(new NumberRandomizer());
        return new InstanceBuildingArguments(randomizers);
    }

    private class SimpleEntity
    {
        public string? Text { get; set; }
        public int Number { get; set; }
    }

    private class MoreComplexEntity : SimpleEntity
    {
        public MoreComplexEntity(string text)
        {
            this.Text = text;
        }
    }

    private class MostComplexEntity : MoreComplexEntity
    {
        public MostComplexEntity(string text) : base(text)
        {
        }

        public MostComplexEntity(string text, int number) : this(text)
        {
            this.Number = number;
        }
    }

    private class TooComplexEntity : MostComplexEntity
    {
        public TooComplexEntity(string text, int number, object other) : base(text, number)
        {
        }
    }
}