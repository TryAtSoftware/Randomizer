namespace TryAtSoftware.Randomizer.Core.Interfaces;

using System;
using System.Linq.Expressions;

public interface IComplexRandomizer<out TEntity> : IRandomizer<TEntity>
{
    IInstanceBuilder<TEntity> InstanceBuilder { get; }
}

public interface IConfigurableComplexRandomizer<TEntity> : IComplexRandomizer<TEntity>
{
    void Randomize(IRandomizationRule<TEntity> rule);
    void Randomize<TValue>(Expression<Func<TEntity, TValue>> propertySelector, IRandomizer<TValue> randomizer);
    void Randomize<TValue>(Expression<Func<TEntity, TValue>> propertySelector, Func<TEntity, IRandomizer<TValue>?> getRandomizer);
}