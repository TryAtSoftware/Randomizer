namespace TryAtSoftware.Randomizer.Core.Interfaces;

public interface IComplexRandomizer<out TEntity> : IRandomizer<TEntity>
{
    IInstanceBuilder<TEntity> InstanceBuilder { get; }
    
    void Randomize(IRandomizationRule<TEntity> rule);
}
