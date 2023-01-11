namespace TryAtSoftware.Randomizer.Core.Primitives;

using TryAtSoftware.Randomizer.Core.Helpers;
using TryAtSoftware.Randomizer.Core.Interfaces;

public class NumberRandomizer : IRandomizer<int>
{
    private readonly int _min;
    private readonly int _max;

    public NumberRandomizer(int min = 0, int max = 1024)
    {
        this._min = min;
        this._max = max;
    }

    public int PrepareRandomValue() => RandomizationHelper.RandomInteger(this._min, this._max);
}