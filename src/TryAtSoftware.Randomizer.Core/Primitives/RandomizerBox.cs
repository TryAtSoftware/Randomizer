namespace TryAtSoftware.Randomizer.Core.Primitives
{
    using System;
    using JetBrains.Annotations;
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public class RandomizerBox<TValue> : IRandomizer<object>
    {
        private readonly IRandomizer<TValue> _randomizer;

        public RandomizerBox([NotNull] IRandomizer<TValue> randomizer)
        {
            this._randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
        }

        public object PrepareRandomValue() => this._randomizer.PrepareRandomValue();
    }
}