namespace TryAtSoftware.Randomizer.Core;

using JetBrains.Annotations;
using System;
using TryAtSoftware.Randomizer.Core.Interfaces;

public class RandomValueSetter<TEntity, TValue> : IRandomValueSetter<TEntity>
    where TEntity : class
{
    private readonly string _propertyName;
    private readonly IRandomizer<TValue> _randomizer;
    private readonly IModelInfo<TEntity> _modelInfo;

    public RandomValueSetter([NotNull] string propertyName, [NotNull] IRandomizer<TValue> randomizer, [NotNull] IModelInfo<TEntity> modelInfo)
    {
        this._propertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
        this._randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
        this._modelInfo = modelInfo ?? throw new ArgumentNullException(nameof(modelInfo));
    }

    public void SetValue(TEntity instance)
    {
        if (instance is null) return;

        var setter = this._modelInfo.GetSetter(this._propertyName);
        if (setter is null) return;

        var value = this._randomizer.PrepareRandomValue();
        setter(instance, value);
    }
}