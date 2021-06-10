namespace TryAtSoftware.Randomizer.Core.Primitives
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using TryAtSoftware.Randomizer.Core.Helpers;
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public class CollectionRandomizer<T> : IRandomizer<IEnumerable<T>>
    {
        [NotNull]
        private readonly IRandomizer<T> _singleValueRandomizer;

        public CollectionRandomizer([NotNull] IRandomizer<T> singleValueRandomizer)
        {
            this._singleValueRandomizer = singleValueRandomizer ?? throw new ArgumentNullException(nameof(singleValueRandomizer));
        }

        public IEnumerable<T> PrepareRandomValue()
        {
            var randomNumber = RandomizationHelper.RandomInteger(1, 10);
            var collection = new List<T>(capacity: randomNumber);

            for (var i = 0; i < randomNumber; i++)
            {
                var currentRandomValue = this._singleValueRandomizer.PrepareRandomValue();
                collection.Add(currentRandomValue);
            }

            return collection;
        }
    }
}