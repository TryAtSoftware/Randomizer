namespace TryAtSoftware.Randomizer.Core
{
    using System;
    using System.Linq.Expressions;
    using JetBrains.Annotations;
    using TryAtSoftware.Randomizer.Core.Helpers;
    using TryAtSoftware.Randomizer.Core.Interfaces;
    using TryAtSoftware.Randomizer.Core.Primitives;

    public class ParameterRandomizationRule<TEntity, TValue> : IParameterRandomizationRule
    {
        public ParameterRandomizationRule([NotNull] Expression<Func<TEntity, TValue>> propertySelector, [NotNull] IRandomizer<TValue> randomizer)
        {
            if (propertySelector is null) throw new ArgumentNullException(nameof(propertySelector));

            if (randomizer is null) throw new ArgumentNullException(nameof(randomizer));

            var property = propertySelector.GetPropertyInfo();
            this.PropertyName = property.Name;
            this.Randomizer = new RandomizerBox<TValue>(randomizer);
        }

        public string PropertyName { get; }
        public IRandomizer<object> Randomizer { get; }
    }
}