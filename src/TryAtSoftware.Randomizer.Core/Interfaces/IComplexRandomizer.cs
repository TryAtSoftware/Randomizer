namespace TryAtSoftware.Randomizer.Core.Interfaces;

public interface IComplexRandomizer<out TEntity> : IRandomizer<TEntity>
{
    IInstanceBuilder<TEntity> InstanceBuilder { get; }

    void AddRandomizationRule(IRandomizationRule<TEntity> rule);
    void OverrideRandomizationRule(IRandomizationRule<TEntity> rule);
}