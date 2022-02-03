namespace TryAtSoftware.Randomizer.Core.Interfaces
{
    public interface IParameterRandomizationRule
    {
        string PropertyName { get; }

        IRandomizer<object> Randomizer { get; }
    }
}