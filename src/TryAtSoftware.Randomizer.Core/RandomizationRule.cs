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
        public RandomizationRule([NotNull] Expression<Func<TEntity, TValue>> propertySelector, [NotNull] IRandomizer<TValue> randomizer)
        {
            var property = propertySelector.GetPropertyInfo();
            this.PropertyName = property.Name;
            this.Randomizer = randomizer;
        }

        /// <inheritdoc />
        public string PropertyName { get; }

        /// <inheritdoc />
        public IRandomizer<TValue> Randomizer { get; }

        /// <inheritdoc />
        public IRandomValueSetter<TEntity> GetValueSetter() 
            => new RandomValueSetter<TEntity,TValue>(this.PropertyName, this.Randomizer, MembersBinderCache<TEntity>.Binder);
    }
}