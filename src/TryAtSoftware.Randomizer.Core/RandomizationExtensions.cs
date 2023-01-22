namespace TryAtSoftware.Randomizer.Core;

using System;
using System.Linq.Expressions;
using TryAtSoftware.Randomizer.Core.Interfaces;
using TryAtSoftware.Randomizer.Core.Primitives;

public static class RandomizationExtensions
{
    public static IRandomizer<TValue> AsConstantRandomizer<TValue>(this TValue value) => new ConstantValueRandomizer<TValue>(value);

    public static void AddRandomizationRule<TEntity, TValue>(
        this IComplexRandomizer<TEntity> complexRandomizer,
        Expression<Func<TEntity, TValue>> propertySelector,
        IRandomizer<TValue> randomizer)
    {
        var rule = BuildRandomizationRule(propertySelector, randomizer);
        complexRandomizer.AddRandomizationRule(rule);
    }
    
    public static void AddRandomizationRule<TEntity, TValue>(
        this IComplexRandomizer<TEntity> complexRandomizer,
        Expression<Func<TEntity, TValue>> propertySelector,
        Func<TEntity, IRandomizer<TValue>?> getRandomizer)
    {
        var rule = BuildRandomizationRule(propertySelector, getRandomizer);
        complexRandomizer.AddRandomizationRule(rule);
    }

    public static void OverrideRandomizationRule<TEntity, TValue>(
        this IComplexRandomizer<TEntity> complexRandomizer,
        Expression<Func<TEntity, TValue>> propertySelector,
        IRandomizer<TValue> randomizer)
    {
        var rule = BuildRandomizationRule(propertySelector, randomizer);
        complexRandomizer.OverrideRandomizationRule(rule);
    }

    public static void OverrideRandomizationRule<TEntity, TValue>(
        this IComplexRandomizer<TEntity> complexRandomizer,
        Expression<Func<TEntity, TValue>> propertySelector,
        Func<TEntity, IRandomizer<TValue>?> getRandomizer)
    {
        var rule = BuildRandomizationRule(propertySelector, getRandomizer);
        complexRandomizer.OverrideRandomizationRule(rule);
    }

    private static IRandomizationRule<TEntity> BuildRandomizationRule<TEntity, TValue>(Expression<Func<TEntity, TValue>> propertySelector, IRandomizer<TValue> randomizer)
        => new RandomizationRule<TEntity, TValue>(propertySelector, randomizer);

    private static IRandomizationRule<TEntity> BuildRandomizationRule<TEntity, TValue>(Expression<Func<TEntity, TValue>> propertySelector, Func<TEntity, IRandomizer<TValue>?> getRandomizer)
        => new RandomizationRule<TEntity, TValue>(propertySelector, getRandomizer);
}