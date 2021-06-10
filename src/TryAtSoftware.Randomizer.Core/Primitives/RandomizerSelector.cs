namespace TryAtSoftware.Randomizer.Core.Primitives
{
    using System.Collections.Generic;
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public class RandomizerSelector<T> : IRandomizer<T>
    {
        private readonly IRandomizer<IRandomizer<T>> _randomizerSelector;

        public RandomizerSelector(IReadOnlyList<IRandomizer<T>> randomizers)
        {
            this._randomizerSelector = new ArrayElementRandomizer<IRandomizer<T>>(randomizers);
        }

        public T PrepareRandomValue()
        {
            var randomRandomizer = this._randomizerSelector.PrepareRandomValue();
            if (randomRandomizer is null)
                return default;

            return randomRandomizer.PrepareRandomValue();
        }
    }
}