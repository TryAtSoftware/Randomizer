namespace TryAtSoftware.Randomizer.Core.Interfaces
{
    public interface IRandomizationRule<in TEntity>
        where TEntity : class
    {
        string PropertyName { get; }

        IRandomValueSetter<TEntity> GetValueSetter();
        IRandomizer<object> GetParameterRandomizer();
    }
}