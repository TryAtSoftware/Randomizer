namespace TryAtSoftware.Randomizer.Core
{
    using System;
    using System.Linq.Expressions;
    using JetBrains.Annotations;
    using TryAtSoftware.Randomizer.Core.Interfaces;
    using TryAtSoftware.Randomizer.Core.Primitives;

    public static class RandomizationExtensions
    {
        public static IRandomizer<TValue> AsConstantRandomizer<TValue>(this TValue value) => new ConstantValueRandomizer<TValue>(value);

        public static void AddRandomizationRule<TEntity, TValue>(
            [NotNull] this IComplexRandomizer<TEntity> complexRandomizer,
            [NotNull] Expression<Func<TEntity, TValue>> propertySelector,
            [NotNull] IRandomizer<TValue> randomizer)
            where TEntity : class
        {
            var rule = BuildRandomizationRule(propertySelector, randomizer);
            complexRandomizer.AddRandomizationRule(rule);
        }

        public static void OverrideRandomizationRule<TEntity, TValue>(
            [NotNull] this IComplexRandomizer<TEntity> complexRandomizer,
            [NotNull] Expression<Func<TEntity, TValue>> propertySelector,
            [NotNull] IRandomizer<TValue> randomizer)
            where TEntity : class
        {
            var rule = BuildRandomizationRule(propertySelector, randomizer);
            complexRandomizer.OverrideRandomizationRule(rule);
        }

        private static IRandomizationRule<TEntity, TValue> BuildRandomizationRule<TEntity, TValue>(Expression<Func<TEntity, TValue>> propertySelector, IRandomizer<TValue> randomizer)
            where TEntity : class
            => new RandomizationRule<TEntity, TValue>(propertySelector, randomizer);
    }
}