namespace TryAtSoftware.Randomizer.Core.Interfaces;

using System;
using System.Collections.Generic;
using TryAtSoftware.Extensions.Reflection.Interfaces;

public interface IModelInfo<TEntity>
{
    IMembersBinder MembersBinder { get; }
    
    Dictionary<string, Action<TEntity, object>> Setters { get; }
}