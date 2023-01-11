namespace TryAtSoftware.Randomizer.Core.Interfaces;

public interface IRandomValueSetter<in TEntity>
    where TEntity : class
{
    void SetValue(TEntity instance);
}