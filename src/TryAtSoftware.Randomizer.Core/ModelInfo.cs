namespace TryAtSoftware.Randomizer.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TryAtSoftware.Extensions.Reflection;
using TryAtSoftware.Randomizer.Core.Interfaces;

// NOTE: Tony Troeff, 18/04/2021 - This is the idea of this class - to provide a single `IMembersBinder` instance for any requested type represented by the generic parameter.
public class ModelInfo<TEntity> : IModelInfo<TEntity>
{
    private readonly Dictionary<string, Action<TEntity, object?>> _setters = new(StringComparer.OrdinalIgnoreCase);
    private readonly List<(ParameterInfo[] Parameters, Func<object?[], TEntity> ObjectInitializer)> _constructors = new();

    private ModelInfo()
    {
        this.RegisterAllProperties();
        this.RegisterAllConstructors();
    }

    public static ModelInfo<TEntity> Instance { get; } = Initialize();

    public IReadOnlyCollection<(ParameterInfo[] Parameters, Func<object?[], TEntity> ObjectInitializer)> Constructors => this._constructors.AsReadOnly();

    public Action<TEntity, object?>? GetSetter(string propertyName)
    {
        if (this._setters.TryGetValue(propertyName, out var setter)) return setter;
        return null;
    }

    private static ModelInfo<TEntity> Initialize() => new();

    private static string PrepareConstructorKey(ConstructorInfo constructorInfo)
    {
        var parameterTypeNames = constructorInfo.GetParameters().Select(p => TypeNames.Get(p.ParameterType));
        return $"Constructor[{string.Join(", ", parameterTypeNames)}]";
    }

    private void RegisterAllProperties()
    {
        var propertiesBinder = new MembersBinder<TEntity>(x => x is PropertyInfo { CanWrite: true }, BindingFlags.Public | BindingFlags.Instance);

        foreach (var (propertyName, memberInfo) in propertiesBinder.MemberInfos)
        {
            var propertyInfo = (PropertyInfo)memberInfo;
            var setterExpression = propertyInfo.ConstructPropertySetter<TEntity, object?>();
            this._setters[propertyName] = setterExpression.Compile();
        }
    }

    private void RegisterAllConstructors()
    {
        if (typeof(TEntity).IsAbstract) return;

        var constructorsBinder = new MembersBinder<TEntity>(x => x is ConstructorInfo, x => PrepareConstructorKey((ConstructorInfo)x), BindingFlags.Public | BindingFlags.Instance);

        foreach (var (_, memberInfo) in constructorsBinder.MemberInfos)
        {
            var constructorInfo = (ConstructorInfo)memberInfo;
            var parameters = constructorInfo.GetParameters();
            var objectInitializer = constructorInfo.ConstructObjectInitializer<TEntity>();

            var data = (parameters, objectInitializer.Compile());
            this._constructors.Add(data);
        }
    }
}