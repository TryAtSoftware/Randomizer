namespace TryAtSoftware.Randomizer.Core.Interfaces;

public interface IRandomValueSetter<in TEntity>
{
    void SetValue(TEntity instance);
}