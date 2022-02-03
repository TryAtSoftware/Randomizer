namespace TryAtSoftware.Randomizer.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public class GeneralInstanceBuilder<TEntity> : IInstanceBuilder<TEntity>
    {
        public TEntity PrepareNewInstance(IInstanceBuildingArguments arguments)
        {
            var parameters = GetParameters(arguments);

            var instance = Activator.CreateInstance(typeof(TEntity), parameters);
            if (instance is TEntity entity) return entity;

            return default;
        }

        private static object[] GetParameters(IInstanceBuildingArguments arguments)
        {
            var constructors = typeof(TEntity).GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            var constructorParameters = constructors.Select(c => c.GetParameters());
            foreach (var parameters in constructorParameters.OrderByDescending(x => x.Length))
            {
                if (parameters.All(p => arguments.ContainsParameter(p.Name))) return parameters.Select(p => arguments.GetParameterValue(p.Name)).ToArray();
            }

            return Array.Empty<object>();
        }
    }
}