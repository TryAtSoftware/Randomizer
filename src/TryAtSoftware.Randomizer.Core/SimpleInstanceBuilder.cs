namespace TryAtSoftware.Randomizer.Core
{
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public abstract class SimpleInstanceBuilder<TEntity> : IInstanceBuilder<TEntity>
    {
        public TEntity PrepareNewInstance(IInstanceBuildingArguments arguments) => this.PrepareNewInstance();

        protected abstract TEntity PrepareNewInstance();
    }
}