namespace TryAtSoftware.Randomizer.Core;

using System;
using System.Linq.Expressions;
using TryAtSoftware.Randomizer.Core.Interfaces;
using TryAtSoftware.Randomizer.Core.Primitives;

public static class RandomizationExtensions
{
    public static IRandomizer<TValue> AsConstantRandomizer<TValue>(this TValue value) => new ConstantValueRandomizer<TValue>(value);
    
    public static void Randomize<TEntity, TValue>(this IComplexRandomizer<TEntity> complexRandomizer, Expression<Func<TEntity, TValue>> propertySelector, IRandomizer<TValue> randomizer)
    {
        if (complexRandomizer is null) throw new ArgumentNullException(nameof(complexRandomizer));
        if (propertySelector is null) throw new ArgumentNullException(nameof(propertySelector));
        if (randomizer is null) throw new ArgumentNullException(nameof(randomizer));
        
        complexRandomizer.Randomize(new RandomizationRule<TEntity,TValue>(propertySelector, randomizer));
    }

    public static void Randomize<TEntity, TValue>(this IComplexRandomizer<TEntity> complexRandomizer, Expression<Func<TEntity, TValue>> propertySelector, Func<TEntity, IRandomizer<TValue>?> getRandomizer)
    {
        if (complexRandomizer is null) throw new ArgumentNullException(nameof(complexRandomizer));
        if (propertySelector is null) throw new ArgumentNullException(nameof(propertySelector));
        if (getRandomizer is null) throw new ArgumentNullException(nameof(getRandomizer));
        
        complexRandomizer.Randomize(new RandomizationRule<TEntity,TValue>(propertySelector, getRandomizer));
    }
}