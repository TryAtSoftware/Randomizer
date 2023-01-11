namespace TryAtSoftware.Randomizer.Core;

using TryAtSoftware.Randomizer.Core.Interfaces;

public abstract class SimpleInstanceBuilder<TEntity> : IInstanceBuilder<TEntity>
{
    public IInstanceBuildingResult<TEntity> PrepareNewInstance(IInstanceBuildingArguments arguments)
    {
        var entity = this.PrepareNewInstance();
        return new InstanceBuildingResult<TEntity>(entity);
    }

    protected abstract TEntity PrepareNewInstance();
}