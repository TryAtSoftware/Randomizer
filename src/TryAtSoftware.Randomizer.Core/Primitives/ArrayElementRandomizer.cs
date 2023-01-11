namespace TryAtSoftware.Randomizer.Core.Primitives;

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TryAtSoftware.Randomizer.Core.Helpers;
using TryAtSoftware.Randomizer.Core.Interfaces;

public class ArrayElementRandomizer<T> : IRandomizer<T>
{
    [NotNull]
    private readonly IReadOnlyList<T> _array;

    public ArrayElementRandomizer([NotNull] IReadOnlyList<T> array)
    {
        this._array = array ?? throw new ArgumentNullException(nameof(array));
    }

    public T PrepareRandomValue()
    {
        if (this._array.Count == 0)
            return default;

        var randomNumber = RandomizationHelper.RandomInteger(0, this._array.Count);
        return this._array[randomNumber];
    }
}