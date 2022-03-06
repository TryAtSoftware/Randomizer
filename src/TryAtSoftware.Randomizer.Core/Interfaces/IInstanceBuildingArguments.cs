namespace TryAtSoftware.Randomizer.Core.Interfaces
{
    public interface IInstanceBuildingArguments
    {
        bool ContainsParameter(string parameterName);
        object GetParameterValue(string parameterName);
    }
}