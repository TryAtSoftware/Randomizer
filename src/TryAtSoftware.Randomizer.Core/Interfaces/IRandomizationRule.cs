namespace TryAtSoftware.Randomizer.Core.Interfaces
{
    public interface IRandomizationRule<in TEntity, out TValue>
        where TEntity : class
    {
        string PropertyName { get; }
        IRandomizer<TValue> Randomizer { get; }

        IRandomValueSetter<TEntity> GetValueSetter();
    }
}