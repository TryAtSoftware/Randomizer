namespace TryAtSoftware.Randomizer.Core.Primitives;

using System;
using TryAtSoftware.Randomizer.Core.Interfaces;

public class RandomizerBox<TOld> : IRandomizer<object?>
{
    private readonly IRandomizer<TOld> _randomizer;

    public RandomizerBox(IRandomizer<TOld> randomizer)
    {
        this._randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
    }

    public object? PrepareRandomValue() => this._randomizer.PrepareRandomValue();
}