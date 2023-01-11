namespace TryAtSoftware.Randomizer.Core;

using System;
using System.Collections.Generic;
using System.Reflection;
using TryAtSoftware.Extensions.Reflection;
using TryAtSoftware.Extensions.Reflection.Interfaces;
using TryAtSoftware.Randomizer.Core.Interfaces;

// NOTE: Tony Troeff, 18/04/2021 - This is the idea of this class - to provide a single `IMembersBinder` instance for any requested type represented by the generic parameter.
public class ModelInfo<TEntity> : IModelInfo<TEntity>
{
    public static ModelInfo<TEntity> Instance { get; } = Initialize();
    
    public ModelInfo()
    {
        this.MembersBinder = new MembersBinder<TEntity>(x => x is PropertyInfo { CanWrite: true }, BindingFlags.Public | BindingFlags.Instance);
        this.Setters = new ();

        foreach (var (propertyName, memberInfo) in this.MembersBinder.MemberInfos)
        {
            if (memberInfo is not PropertyInfo propertyInfo) continue;

            var setterExpression = propertyInfo.ConstructPropertySetter<TEntity, object>();
            this.Setters[propertyName] = setterExpression.Compile();
        }
    }

    public IMembersBinder MembersBinder { get; }
    public Dictionary<string, Action<TEntity, object>> Setters { get; }

    private static ModelInfo<TEntity> Initialize() => new ();
}