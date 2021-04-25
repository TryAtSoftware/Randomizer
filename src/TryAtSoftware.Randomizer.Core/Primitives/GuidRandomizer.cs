namespace TryAtSoftware.Randomizer.Core.Primitives
{
    using System;
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public class GuidRandomizer : IRandomizer<Guid>
    {
        public Guid PrepareRandomValue() => Guid.NewGuid();
    }
}