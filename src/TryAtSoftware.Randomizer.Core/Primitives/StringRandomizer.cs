namespace TryAtSoftware.Randomizer.Core.Primitives
{
    using TryAtSoftware.Randomizer.Core;
    using TryAtSoftware.Randomizer.Core.Helpers;
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public class StringRandomizer : IRandomizer<string>
    {
        /// <inheritdoc />
        public string PrepareRandomValue() => RandomizationHelper.GetRandomString();
    }
}