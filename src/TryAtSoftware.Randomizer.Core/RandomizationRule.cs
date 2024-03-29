﻿namespace TryAtSoftware.Randomizer.Core;

using System;
using System.Linq.Expressions;
using TryAtSoftware.Extensions.Reflection;
using TryAtSoftware.Randomizer.Core.Interfaces;
using TryAtSoftware.Randomizer.Core.Primitives;

public class RandomizationRule<TEntity, TValue> : IRandomizationRule<TEntity>
{
    private readonly IRandomValueSetter<TEntity> _valueSetter;
    private readonly IRandomizer<TValue>? _parameterRandomizer;

    public RandomizationRule(Expression<Func<TEntity, TValue>> propertySelector, IRandomizer<TValue> randomizer)
    {
        if (randomizer is null) throw new ArgumentNullException(nameof(randomizer));

        this.PropertyName = ExtractPropertyName(propertySelector);
        this._valueSetter = new RandomValueSetter<TEntity, TValue>(this.PropertyName, _ => randomizer, ModelInfo<TEntity>.Instance);
        this._parameterRandomizer = randomizer;
    }

    public RandomizationRule(Expression<Func<TEntity, TValue>> propertySelector, Func<TEntity, IRandomizer<TValue>?> getRandomizer, IRandomizer<TValue>? parameterRandomizer = null)
    {
        if (getRandomizer is null) throw new ArgumentNullException(nameof(getRandomizer));

        this.PropertyName = ExtractPropertyName(propertySelector);
        this._valueSetter = new RandomValueSetter<TEntity, TValue>(this.PropertyName, getRandomizer, ModelInfo<TEntity>.Instance);
        this._parameterRandomizer = parameterRandomizer;
    }

    public RandomizationRule(Expression<Func<TEntity, TValue>> propertySelector, IRandomValueSetter<TEntity> valueSetter, IRandomizer<TValue>? parameterRandomizer = null)
    {
        this.PropertyName = ExtractPropertyName(propertySelector);
        this._valueSetter = valueSetter ?? throw new ArgumentNullException(nameof(valueSetter));
        this._parameterRandomizer = parameterRandomizer;
    }

    /// <inheritdoc />
    public string PropertyName { get; }

    /// <inheritdoc />
    public IRandomValueSetter<TEntity> GetValueSetter() => this._valueSetter;

    public IRandomizer<object?>? GetParameterRandomizer()
    {
        if (this._parameterRandomizer is null) return null;
        return new RandomizerBox<TValue>(this._parameterRandomizer);
    }

    private static string ExtractPropertyName(Expression<Func<TEntity, TValue>> propertySelector)
    {
        if (propertySelector is null) throw new ArgumentNullException(nameof(propertySelector));

        var memberInfo = propertySelector.GetMemberInfo();
        return memberInfo.Name;
    }
}