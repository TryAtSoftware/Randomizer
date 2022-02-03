namespace TryAtSoftware.Randomizer.Core.Interfaces
{
    public interface IInstanceBuilder<out TEntity>
    {
        TEntity PrepareNewInstance(IInstanceBuildingArguments arguments);
    }
}