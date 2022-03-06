namespace TryAtSoftware.Randomizer.Core.Tests
{
    using System.Linq;
    using TryAtSoftware.Randomizer.Core.Helpers;
    using TryAtSoftware.Randomizer.Core.Primitives;
    using Xunit;

    public static class InstanceBuildingResultTests
    {
        [Fact]
        public static void InstanceBuildingResultShouldBeCorrectlyInstantiated()
        {
            var randomText = RandomizationHelper.GetRandomString();
            var instanceBuildingResult = new InstanceBuildingResult<string>(randomText);

            Assert.Equal(randomText, instanceBuildingResult.Instance);
        }

        [Fact]
        public static void InstanceBuildingResultShouldBeCorrectlyInstantiatedWithNull()
        {
            var instanceBuildingResult = new InstanceBuildingResult<string>(null);
            Assert.Null(instanceBuildingResult.Instance);
        }

        [Fact]
        public static void InstanceBuildingResultShouldCorrectlyDefineTheUsedParametersDuringInstantiation()
        {
            var parametersRandomizer = new CollectionRandomizer<string>(new StringRandomizer());
            var randomParameters = parametersRandomizer.PrepareRandomValue().ToList();

            var randomText = RandomizationHelper.GetRandomString();
            var instanceBuildingResult = new InstanceBuildingResult<string>(randomText, randomParameters);
            Assert.Equal(randomText, instanceBuildingResult.Instance);
            foreach (var randomParameter in randomParameters) Assert.True(instanceBuildingResult.IsUsed(randomParameter));

            var otherRandomParameters = parametersRandomizer.PrepareRandomValue();
            foreach (var otherRandomParameter in otherRandomParameters) Assert.False(instanceBuildingResult.IsUsed(otherRandomParameter));
        }

        [Theory]
        [MemberData(nameof(TestsHelper.GetInvalidStringParameters), MemberType = typeof(TestsHelper))]
        public static void IsUsedWithEmptyParameterNameShouldAlwaysReturnFalse(string parameterName)
        {
            var randomText = RandomizationHelper.GetRandomString();
            var instanceBuildingResult = new InstanceBuildingResult<string>(randomText);

            Assert.Equal(randomText, instanceBuildingResult.Instance);
            Assert.False(instanceBuildingResult.IsUsed(parameterName));
        }
    }
}