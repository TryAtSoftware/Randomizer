namespace TryAtSoftware.Randomizer.Core.Primitives
{
    using TryAtSoftware.Randomizer.Core.Helpers;
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public class BooleanRandomizer : IRandomizer<bool>
    {
        /// <inheritdoc />
        public bool PrepareRandomValue() => RandomizationHelper.RandomProbability();
    }
}