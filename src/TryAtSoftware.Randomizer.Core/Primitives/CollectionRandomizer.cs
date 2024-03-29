﻿namespace TryAtSoftware.Randomizer.Core.Primitives;

using System;
using System.Collections.Generic;
using TryAtSoftware.Randomizer.Core.Helpers;
using TryAtSoftware.Randomizer.Core.Interfaces;

public class CollectionRandomizer<T> : IRandomizer<IEnumerable<T>>
{
    private readonly IRandomizer<T> _singleValueRandomizer;

    private readonly int _minLength;
    private readonly int _maxLength;

    public CollectionRandomizer(IRandomizer<T> singleValueRandomizer, int minLength = 1, int maxLength = 10)
    {
        this._singleValueRandomizer = singleValueRandomizer ?? throw new ArgumentNullException(nameof(singleValueRandomizer));
        this._minLength = minLength;
        this._maxLength = maxLength;
    }

    public IEnumerable<T> PrepareRandomValue()
    {
        var length = RandomizationHelper.RandomInteger(this._minLength, this._maxLength + 1);
        var collection = new List<T>(capacity: length);

        for (var i = 0; i < length; i++)
        {
            var currentRandomValue = this._singleValueRandomizer.PrepareRandomValue();
            collection.Add(currentRandomValue);
        }

        return collection;
    }
}