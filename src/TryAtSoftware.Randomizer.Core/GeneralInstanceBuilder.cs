namespace TryAtSoftware.Randomizer.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public class GeneralInstanceBuilder<TEntity> : IInstanceBuilder<TEntity>
    {
        public IInstanceBuildingResult<TEntity> PrepareNewInstance(IInstanceBuildingArguments arguments)
        {
            var (parameters, usedParameterNames) = GetParameters(arguments);

            var instance = Activator.CreateInstance(typeof(TEntity), parameters);
            if (instance is TEntity entity) return new InstanceBuildingResult<TEntity>(entity, usedParameterNames);

            return new InstanceBuildingResult<TEntity>(default);
        }

        private static (object[] Parameters, string[] UsedParameterNames) GetParameters(IInstanceBuildingArguments arguments)
        {
            var constructors = typeof(TEntity).GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            var constructorParameters = constructors.Select(c => c.GetParameters());
            foreach (var parameters in constructorParameters.OrderByDescending(x => x.Length))
            {
                if (!parameters.All(p => arguments.ContainsParameter(p.Name))) continue;

                var parameterValues = new object[parameters.Length];
                var parameterNames = new string[parameters.Length];
                for (var i = 0; i < parameters.Length; i++)
                {
                    parameterValues[i] = arguments.GetParameterValue(parameters[i].Name);
                    parameterNames[i] = parameters[i].Name;
                }

                return (Parameters: parameterValues, UsedParameterNames: parameterNames);
            }

            return (Parameters: Array.Empty<object>(), UsedParameterNames: Array.Empty<string>());
        }
    }
}