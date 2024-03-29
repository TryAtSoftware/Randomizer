namespace TryAtSoftware.Randomizer.Core.Primitives;

using System;
using TryAtSoftware.Randomizer.Core.Interfaces;

public class RandomizerConverter<TOld, TNew> : IRandomizer<TNew>
    where TOld : IConvertible?
{
    private readonly IRandomizer<TOld> _randomizer;

    public RandomizerConverter(IRandomizer<TOld> randomizer)
    {
        this._randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
    }

    public TNew PrepareRandomValue() => (TNew)Convert.ChangeType(this._randomizer.PrepareRandomValue(), typeof(TNew));
}