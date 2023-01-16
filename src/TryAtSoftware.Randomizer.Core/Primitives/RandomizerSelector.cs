namespace TryAtSoftware.Randomizer.Core.Primitives;

using TryAtSoftware.Randomizer.Core.Interfaces;

public class RandomizerSelector<T> : IRandomizer<T>
{
    private readonly IRandomizer<IRandomizer<T>> _randomizerSelector;

    public RandomizerSelector(IRandomizer<T>[] randomizers)
    {
        this._randomizerSelector = new ArrayElementRandomizer<IRandomizer<T>>(randomizers);
    }

    public T PrepareRandomValue()
    {
        var randomRandomizer = this._randomizerSelector.PrepareRandomValue();
        return randomRandomizer.PrepareRandomValue();
    }
}