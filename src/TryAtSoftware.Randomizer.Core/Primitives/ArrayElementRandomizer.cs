namespace TryAtSoftware.Randomizer.Core.Primitives;

using System;
using TryAtSoftware.Randomizer.Core.Helpers;
using TryAtSoftware.Randomizer.Core.Interfaces;

public class ArrayElementRandomizer<T> : IRandomizer<T>
{
    private readonly T[] _array;

    public ArrayElementRandomizer(T[] array)
    {
        if (array is null) throw new ArgumentNullException(nameof(array));
        if (array.Length == 0) throw new ArgumentException("The provided array must not be empty.", nameof(array));
        this._array = array;
    }

    public T PrepareRandomValue()
    {
        var randomNumber = RandomizationHelper.RandomInteger(0, this._array.Length);
        return this._array[randomNumber];
    }
}