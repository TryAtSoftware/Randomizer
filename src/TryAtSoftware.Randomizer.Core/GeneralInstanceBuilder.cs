namespace TryAtSoftware.Randomizer.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TryAtSoftware.Randomizer.Core.Interfaces;

public class GeneralInstanceBuilder<TEntity> : IInstanceBuilder<TEntity>
{
    private readonly IModelInfo<TEntity> _modelInfo;

    public GeneralInstanceBuilder(IModelInfo<TEntity>? modelInfo = null)
    {
        this._modelInfo = modelInfo ?? ModelInfo<TEntity>.Instance;
    }

    public IInstanceBuildingResult<TEntity> PrepareNewInstance(IInstanceBuildingArguments arguments)
    {
        var constructorsData = this._modelInfo.Constructors;

        foreach (var (parameters, objectInitializer) in constructorsData.OrderByDescending(x => x.Parameters.Length))
        {
            if (!CanConstructInstance(arguments, parameters)) continue;

            var newInstanceData = ConstructNewInstance(arguments, parameters, objectInitializer);
            return new InstanceBuildingResult<TEntity>(newInstanceData.Instance, newInstanceData.UsedParameterNames);
        }

        return new InstanceBuildingResult<TEntity>(default);
    }

    private static bool CanConstructInstance(IInstanceBuildingArguments arguments, ParameterInfo[] constructorParameters)
        => Array.TrueForAll(constructorParameters, x => x.HasDefaultValue || arguments.ContainsParameter(x.Name));

    private static (TEntity Instance, HashSet<string> UsedParameterNames) ConstructNewInstance(IInstanceBuildingArguments arguments, ParameterInfo[] parameters, Func<object?[], TEntity> objectInitializer)
    {
        var values = new object?[parameters.Length];
        var parameterNames = new HashSet<string>();

        for (var i = 0; i < parameters.Length; i++)
        {
            object? currentValue = null;
            if (arguments.ContainsParameter(parameters[i].Name))
            {
                currentValue = arguments.GetParameterValue(parameters[i].Name);
                parameterNames.Add(parameters[i].Name);
            }

            values[i] = currentValue;
        }

        var instance = objectInitializer(values);
        return (instance, parameterNames);
    }
}