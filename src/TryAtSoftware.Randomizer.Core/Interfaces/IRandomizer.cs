namespace TryAtSoftware.Randomizer.Core.Interfaces
{
    public interface IRandomizer<out TValue>
    {
        TValue PrepareRandomValue();
    }
}