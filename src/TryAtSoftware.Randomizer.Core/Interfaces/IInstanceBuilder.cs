namespace TryAtSoftware.Randomizer.Core.Interfaces;

public interface IInstanceBuilder<out TEntity>
{
    IInstanceBuildingResult<TEntity> PrepareNewInstance(IInstanceBuildingArguments arguments);
}