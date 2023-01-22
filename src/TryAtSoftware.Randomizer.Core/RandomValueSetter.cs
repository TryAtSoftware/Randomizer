namespace TryAtSoftware.Randomizer.Core;

using System;
using TryAtSoftware.Randomizer.Core.Interfaces;

public class RandomValueSetter<TEntity, TValue> : IRandomValueSetter<TEntity>
{
    private readonly string _propertyName;
    private readonly Func<TEntity, IRandomizer<TValue>?> _getRandomizer;
    private readonly IModelInfo<TEntity> _modelInfo;

    public RandomValueSetter(string propertyName, Func<TEntity, IRandomizer<TValue>?> getRandomizer, IModelInfo<TEntity> modelInfo)
    {
        if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException(nameof(propertyName));
        this._propertyName = propertyName;

        this._getRandomizer = getRandomizer ?? throw new ArgumentNullException(nameof(getRandomizer));
        this._modelInfo = modelInfo ?? throw new ArgumentNullException(nameof(modelInfo));
    }

    public void SetValue(TEntity instance)
    {
        if (instance is null) return;

        var setter = this._modelInfo.GetSetter(this._propertyName);
        if (setter is null) return;

        var randomizer = this._getRandomizer(instance);
        if (randomizer is null) return;
        
        var value = randomizer.PrepareRandomValue();
        setter(instance, value);
    }
}