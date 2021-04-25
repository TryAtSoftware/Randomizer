namespace TryAtSoftware.Randomizer.Core
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public class ComplexRandomizer<TEntity> : IComplexRandomizer<TEntity>
        where TEntity : class
    {
        private readonly Dictionary<string, IRandomValueSetter<TEntity>> _randomValueSetters = new Dictionary<string, IRandomValueSetter<TEntity>>();

        public ComplexRandomizer([NotNull] IInstanceBuilder<TEntity> instanceBuilder)
        {
            this.InstanceBuilder = instanceBuilder ?? throw new ArgumentNullException(nameof(instanceBuilder));
        }

        /// <inheritdoc />
        public IInstanceBuilder<TEntity> InstanceBuilder { get; }

        /// <inheritdoc />
        public void AddRandomizationRule<TValue>(IRandomizationRule<TEntity, TValue> rule)
        {
            if (string.IsNullOrWhiteSpace(rule?.PropertyName) || this._randomValueSetters.ContainsKey(rule.PropertyName))
                return;

            var setter = rule.GetValueSetter();
            this._randomValueSetters[rule.PropertyName] = setter;
        }

        /// <inheritdoc />
        public void OverrideRandomizationRule<TValue>(IRandomizationRule<TEntity, TValue> rule)
        {
            if (string.IsNullOrWhiteSpace(rule?.PropertyName))
                return;

            var setter = rule.GetValueSetter();
            this._randomValueSetters[rule.PropertyName] = setter;
        }

        /// <inheritdoc />
        public TEntity PrepareRandomValue()
        {
            var instance = this.InstanceBuilder.PrepareNewInstance();
            if (instance is null)
                return null;
            
            foreach (var randomValueSetter in this._randomValueSetters.Values)
                randomValueSetter.SetValue(instance);

            return instance;
        }
    }
}