namespace TryAtSoftware.Randomizer.Core.Primitives
{
    using System;
    using TryAtSoftware.Randomizer.Core.Helpers;
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public class EnumRandomizer<TEnum> : IRandomizer<TEnum>
        where TEnum : Enum
    {
        public TEnum PrepareRandomValue()
        {
            var values = Enum.GetValues(typeof(TEnum));
            var randomIndex = RandomizationHelper.GetRandomNumber(values.Length);
            return (TEnum)values.GetValue(randomIndex);
        }
    }
}