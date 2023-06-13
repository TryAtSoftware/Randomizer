namespace TryAtSoftware.Randomizer.Core;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TryAtSoftware.Randomizer.Core.Interfaces;

public class ComplexRandomizer<TEntity> : IConfigurableComplexRandomizer<TEntity>
{
    private readonly List<string> _orderedMembers = new ();
    private readonly HashSet<string> _randomizedMembers = new ();
    private readonly Dictionary<string, IRandomizer<object?>> _constructorRandomizers = new ();
    private readonly Dictionary<string, IRandomValueSetter<TEntity>> _randomValueSetters = new ();

    public ComplexRandomizer()
        : this(new GeneralInstanceBuilder<TEntity>())
    {
    }

    public ComplexRandomizer(IInstanceBuilder<TEntity> instanceBuilder)
    {
        this.InstanceBuilder = instanceBuilder ?? throw new ArgumentNullException(nameof(instanceBuilder));
    }

    /// <inheritdoc />
    public IInstanceBuilder<TEntity> InstanceBuilder { get; }

    /// <inheritdoc />
    public TEntity PrepareRandomValue()
    {
        var instanceBuildingArguments = new InstanceBuildingArguments(this._constructorRandomizers);
        var instanceBuildingResult = this.InstanceBuilder.PrepareNewInstance(instanceBuildingArguments);
        var instance = instanceBuildingResult.Instance;
        if (instance is null) throw new InvalidOperationException("An object could not be instantiated by using the registered randomization rules.");

        foreach (var name in this._orderedMembers)
        {
            var randomValueSetter = this._randomValueSetters[name];
            if (!instanceBuildingResult.IsUsed(name)) randomValueSetter.SetValue(instance);
        }

        return instance;
    }

    /// <inheritdoc />
    public void Randomize(IRandomizationRule<TEntity> rule)
    {
        if (rule is null) throw new ArgumentNullException(nameof(rule));
        this.SetRuleInternally(rule);
    }

    /// <inheritdoc />
    public void Randomize<TValue>(Expression<Func<TEntity, TValue>> propertySelector, IRandomizer<TValue> randomizer)
    {
        if (propertySelector is null) throw new ArgumentNullException(nameof(propertySelector));
        if (randomizer is null) throw new ArgumentNullException(nameof(randomizer));
        
        this.SetRuleInternally(new RandomizationRule<TEntity,TValue>(propertySelector, randomizer));
    }

    /// <inheritdoc />
    public void Randomize<TValue>(Expression<Func<TEntity, TValue>> propertySelector, Func<TEntity, IRandomizer<TValue>?> getRandomizer)
    {
        if (propertySelector is null) throw new ArgumentNullException(nameof(propertySelector));
        if (getRandomizer is null) throw new ArgumentNullException(nameof(getRandomizer));
        
        this.SetRuleInternally(new RandomizationRule<TEntity,TValue>(propertySelector, getRandomizer));
    }

    private void SetRuleInternally(IRandomizationRule<TEntity> rule)
    {
        if (string.IsNullOrWhiteSpace(rule.PropertyName)) return;

        var setter = rule.GetValueSetter();
        this._randomValueSetters[rule.PropertyName] = setter;

        var parameterRandomizer = rule.GetParameterRandomizer();
        if (parameterRandomizer is null) this._constructorRandomizers.Remove(rule.PropertyName);
        else this._constructorRandomizers[rule.PropertyName] = parameterRandomizer;

        if (this._randomizedMembers.Add(rule.PropertyName)) this._orderedMembers.Add(rule.PropertyName);
    }
}