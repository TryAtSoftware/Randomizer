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

        public static bool RandomizeParameter<TEntity, TValue>(
            [NotNull] this IComplexRandomizer<TEntity> complexRandomizer,
            [NotNull] Expression<Func<TEntity, TValue>> propertySelector,
            [NotNull] IRandomizer<TValue> randomizer)
            where TEntity : class
        {
            var rule = BuildParameterRandomizationRule(propertySelector, randomizer);
            return complexRandomizer.AddParameterRandomizationRule(rule);
        }

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

        private static IParameterRandomizationRule BuildParameterRandomizationRule<TEntity, TValue>(Expression<Func<TEntity, TValue>> propertySelector, IRandomizer<TValue> randomizer)
            where TEntity : class
            => new ParameterRandomizationRule<TEntity, TValue>(propertySelector, randomizer);

        private static IRandomizationRule<TEntity> BuildRandomizationRule<TEntity, TValue>(Expression<Func<TEntity, TValue>> propertySelector, IRandomizer<TValue> randomizer)
            where TEntity : class
            => new RandomizationRule<TEntity, TValue>(propertySelector, randomizer);
    }
}