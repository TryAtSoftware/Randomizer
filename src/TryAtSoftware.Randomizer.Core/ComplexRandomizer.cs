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
        public void AddRandomizationRule(IRandomizationRule<TEntity> rule)
        {
            if (string.IsNullOrWhiteSpace(rule?.PropertyName) || this._randomValueSetters.ContainsKey(rule.PropertyName)) return;
            this.SetRuleInternally(rule);
        }

        /// <inheritdoc />
        public void OverrideRandomizationRule(IRandomizationRule<TEntity> rule)
        {
            if (string.IsNullOrWhiteSpace(rule?.PropertyName)) return;
            this.SetRuleInternally(rule);
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

        private void SetRuleInternally(IRandomizationRule<TEntity> rule)
        {
            if (rule is null) return;

            var setter = rule.GetValueSetter();
            if (setter != null) this._randomValueSetters[rule.PropertyName] = setter;

            var parameterRandomizer = rule.GetParameterRandomizer();
            if (parameterRandomizer != null) this._constructorRandomizers[rule.PropertyName] = parameterRandomizer;
        }
    }
}