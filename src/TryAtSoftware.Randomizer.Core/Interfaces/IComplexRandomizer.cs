namespace TryAtSoftware.Randomizer.Core.Interfaces
{
    using JetBrains.Annotations;

    public interface IComplexRandomizer<out TEntity> : IRandomizer<TEntity>
        where TEntity : class
    {
        [NotNull]
        IInstanceBuilder<TEntity> InstanceBuilder { get; }

        bool RandomizeConstructorParameter<TValue>(string parameterName, IRandomizer<TValue> randomizer);
        bool OverrideConstructorParameterRandomization<TValue>(string parameterName, IRandomizer<TValue> randomizer);
        void AddRandomizationRule(IRandomizationRule<TEntity> rule);
        void OverrideRandomizationRule(IRandomizationRule<TEntity> rule);
    }
}