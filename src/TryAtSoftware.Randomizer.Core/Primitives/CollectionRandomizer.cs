namespace TryAtSoftware.Randomizer.Core.Primitives;

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TryAtSoftware.Randomizer.Core.Helpers;
using TryAtSoftware.Randomizer.Core.Interfaces;

public class CollectionRandomizer<T> : IRandomizer<IEnumerable<T>>
{
    [NotNull]
    private readonly IRandomizer<T> _singleValueRandomizer;

    private readonly int _minLength;
    private readonly int _maxLength;

    public CollectionRandomizer([NotNull] IRandomizer<T> singleValueRandomizer, int minLength = 1, int maxLength = 10)
    {
        this._singleValueRandomizer = singleValueRandomizer ?? throw new ArgumentNullException(nameof(singleValueRandomizer));
        this._minLength = minLength;
        this._maxLength = maxLength;
    }

    public IEnumerable<T> PrepareRandomValue()
    {
        var length = this.GetRandomLength();
        var collection = new List<T>(capacity: length);

        for (var i = 0; i < length; i++)
        {
            var currentRandomValue = this._singleValueRandomizer.PrepareRandomValue();
            collection.Add(currentRandomValue);
        }

        return collection;
    }

    private int GetRandomLength()
    {
        if (this._maxLength == this._minLength) return this._maxLength;
        return RandomizationHelper.RandomInteger(this._minLength, this._maxLength);
    }
}