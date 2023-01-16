namespace TryAtSoftware.Randomizer.Core.Interfaces;

public interface IRandomizationRule<in TEntity>
{
    string PropertyName { get; }

    IRandomValueSetter<TEntity> GetValueSetter();
    IRandomizer<object?>? GetParameterRandomizer();
}