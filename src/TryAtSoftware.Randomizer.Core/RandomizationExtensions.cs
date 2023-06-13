namespace TryAtSoftware.Randomizer.Core;

using TryAtSoftware.Randomizer.Core.Interfaces;
using TryAtSoftware.Randomizer.Core.Primitives;

public static class RandomizationExtensions
{
    public static IRandomizer<TValue> AsConstantRandomizer<TValue>(this TValue value) => new ConstantValueRandomizer<TValue>(value);
}