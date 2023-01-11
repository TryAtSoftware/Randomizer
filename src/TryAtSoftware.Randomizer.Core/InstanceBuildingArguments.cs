namespace TryAtSoftware.Randomizer.Core;

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TryAtSoftware.Randomizer.Core.Interfaces;

public class InstanceBuildingArguments : IInstanceBuildingArguments
{
    [NotNull]
    private readonly IDictionary<string, IRandomizer<object>> _randomizers = new Dictionary<string, IRandomizer<object>>(StringComparer.InvariantCultureIgnoreCase);

    public InstanceBuildingArguments([NotNull] IDictionary<string, IRandomizer<object>> randomizers)
    {
        if (randomizers is null) throw new ArgumentNullException(nameof(randomizers));
        foreach (var (parameterName, randomizer) in randomizers) this._randomizers[parameterName] = randomizer;
    }

    public bool ContainsParameter(string parameterName) => this._randomizers.ContainsKey(parameterName);

    public object GetParameterValue(string parameterName)
    {
        this._randomizers.TryGetValue(parameterName, out var randomizer);
        return randomizer?.PrepareRandomValue();
    }
}