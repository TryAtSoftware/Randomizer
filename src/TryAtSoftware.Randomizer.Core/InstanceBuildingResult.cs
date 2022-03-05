namespace TryAtSoftware.Randomizer.Core
{
    using System.Collections.Generic;
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public class InstanceBuildingResult<TEntity> : IInstanceBuildingResult<TEntity>
    {
        private readonly HashSet<string> _usedParameters = new HashSet<string>();

        public InstanceBuildingResult(TEntity instance, IEnumerable<string> usedParameters = null)
        {
            this.Instance = instance;
            this.RegisterUsedParameters(usedParameters);
        }

        public TEntity Instance { get; }
        public bool IsUsed(string parameterName) => string.IsNullOrWhiteSpace(parameterName) == false && this._usedParameters.Contains(parameterName);

        private void RegisterUsedParameters(IEnumerable<string> usedParameters)
        {
            if (usedParameters is null) return;

            foreach (var parameter in usedParameters)
            {
                if (string.IsNullOrWhiteSpace(parameter) == false) this._usedParameters.Add(parameter);
            }
        }
    }
}