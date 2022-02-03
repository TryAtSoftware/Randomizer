namespace TryAtSoftware.Randomizer.Core
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public class ComplexRandomizer<TEntity> : IComplexRandomizer<TEntity>
        where TEntity : class
    {
        private readonly Dictionary<string, IRandomizer<object>> _constructorRandomizers = new Dictionary<string, IRandomizer<object>>();
        private readonly Dictionary<string, IRandomValueSetter<TEntity>> _randomValueSetters = new Dictionary<string, IRandomValueSetter<TEntity>>();

        public ComplexRandomizer([NotNull] IInstanceBuilder<TEntity> instanceBuilder)
        {
            this.InstanceBuilder = instanceBuilder ?? throw new ArgumentNullException(nameof(instanceBuilder));
        }

        /// <inheritdoc />
        public IInstanceBuilder<TEntity> InstanceBuilder { get; }

        /// <inheritdoc />
        public bool AddParameterRandomizationRule(IParameterRandomizationRule rule)
        {
            if (string.IsNullOrWhiteSpace(rule?.PropertyName) || this._randomValueSetters.ContainsKey(rule.PropertyName)) return false;
            this._constructorRandomizers.Add(rule.PropertyName, rule.Randomizer);
            return true;
        }

        /// <inheritdoc />
        public bool OverrideParameterRandomizationRule(IParameterRandomizationRule rule)
        {
            if (string.IsNullOrWhiteSpace(rule?.PropertyName) || this._randomValueSetters.ContainsKey(rule.PropertyName) == false) return false;
            this._constructorRandomizers[rule.PropertyName] = rule.Randomizer;
            return true;
        }

        /// <inheritdoc />
        public void AddRandomizationRule(IRandomizationRule<TEntity> rule)
        {
            if (string.IsNullOrWhiteSpace(rule?.PropertyName) || this._randomValueSetters.ContainsKey(rule.PropertyName)) return;

            var setter = rule.GetValueSetter();
            this._randomValueSetters[rule.PropertyName] = setter;
        }

        /// <inheritdoc />
        public void OverrideRandomizationRule(IRandomizationRule<TEntity> rule)
        {
            if (string.IsNullOrWhiteSpace(rule?.PropertyName)) return;

            var setter = rule.GetValueSetter();
            this._randomValueSetters[rule.PropertyName] = setter;
        }

        /// <inheritdoc />
        public TEntity PrepareRandomValue()
        {
            var instanceBuildingArguments = new InstanceBuildingArguments(this._constructorRandomizers);
            var instance = this.InstanceBuilder.PrepareNewInstance(instanceBuildingArguments);
            if (instance is null) return null;

            foreach (var randomValueSetter in this._randomValueSetters.Values) randomValueSetter.SetValue(instance);

            return instance;
        }
    }
}