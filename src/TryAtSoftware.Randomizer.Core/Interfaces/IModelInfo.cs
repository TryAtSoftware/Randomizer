namespace TryAtSoftware.Randomizer.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Reflection;

public interface IModelInfo<TEntity>
{
    Action<TEntity, object> GetSetter(string propertyName);
    IReadOnlyCollection<(ParameterInfo[] Parameters, Func<object?[], TEntity> ObjectInitializer)> Constructors { get; }
}