namespace TryAtSoftware.Randomizer.Core.Interfaces
{
    public interface IInstanceBuildingResult<out TEntity>
    {
        TEntity Instance { get; }
        bool IsUsed(string parameterName);

    }
}