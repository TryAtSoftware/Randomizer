namespace TryAtSoftware.Randomizer.Core
{
    using System;
    using System.Linq.Expressions;
    using JetBrains.Annotations;
    using TryAtSoftware.Randomizer.Core.Helpers;
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public class RandomizationRule<TEntity, TValue> : IRandomizationRule<TEntity>
        where TEntity : class
    {
        private readonly IRandomValueSetter<TEntity> _valueSetter;
        
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
        }

        public RandomizationRule([NotNull] Expression<Func<TEntity, TValue>> propertySelector, [NotNull] IRandomValueSetter<TEntity> valueSetter)
            : this(propertySelector)
        {
            this._valueSetter = valueSetter ?? throw new ArgumentNullException(nameof(valueSetter));
        }

        /// <inheritdoc />
        public string PropertyName { get; }

        /// <inheritdoc />
        public IRandomValueSetter<TEntity> GetValueSetter() => this._valueSetter;
    }
}