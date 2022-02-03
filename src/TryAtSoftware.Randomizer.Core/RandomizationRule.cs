namespace TryAtSoftware.Randomizer.Core
{
    using System;
    using System.Linq.Expressions;
    using JetBrains.Annotations;
    using TryAtSoftware.Randomizer.Core.Helpers;
    using TryAtSoftware.Randomizer.Core.Interfaces;
    using TryAtSoftware.Randomizer.Core.Primitives;

    public class RandomizationRule<TEntity, TValue> : IRandomizationRule<TEntity>
        where TEntity : class
    {
        private readonly IRandomValueSetter<TEntity> _valueSetter;
        private readonly IRandomizer<TValue> _parameterRandomizer;

        private RandomizationRule(Expression<Func<TEntity, TValue>> propertySelector)
        {
            if (propertySelector is null)
                throw new ArgumentNullException(nameof(propertySelector));

            var property = propertySelector.GetPropertyInfo();
            this.PropertyName = property.Name;
        }

        public RandomizationRule([NotNull] Expression<Func<TEntity, TValue>> propertySelector, [NotNull] IRandomizer<TValue> randomizer)
            : this(propertySelector)
        {
            if (randomizer is null)
                throw new ArgumentNullException(nameof(randomizer));
            
            this._valueSetter = new RandomValueSetter<TEntity, TValue>(this.PropertyName, randomizer, MembersBinderCache<TEntity>.Binder);
            this._parameterRandomizer = randomizer;
        }

        public RandomizationRule([NotNull] Expression<Func<TEntity, TValue>> propertySelector, [NotNull] IRandomValueSetter<TEntity> valueSetter)
            : this(propertySelector)
        {
            this._valueSetter = valueSetter ?? throw new ArgumentNullException(nameof(valueSetter));
        }

        public RandomizationRule([NotNull] Expression<Func<TEntity, TValue>> propertySelector, [NotNull] IRandomValueSetter<TEntity> valueSetter, [NotNull] IRandomizer<TValue> parameterRandomizer)
            : this(propertySelector, valueSetter)
        {
            this._parameterRandomizer = parameterRandomizer ?? throw new ArgumentNullException(nameof(parameterRandomizer));
        }

        /// <inheritdoc />
        public string PropertyName { get; }

        /// <inheritdoc />
        public IRandomValueSetter<TEntity> GetValueSetter() => this._valueSetter;

        public IRandomizer<object> GetParameterRandomizer() => new RandomizerBox<TValue>(this._parameterRandomizer);
    }
}