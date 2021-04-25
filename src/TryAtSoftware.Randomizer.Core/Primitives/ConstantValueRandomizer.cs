namespace TryAtSoftware.Randomizer.Core.Primitives
{
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public class ConstantValueRandomizer<TValue> : IRandomizer<TValue>
    {
        private readonly TValue _value;

        public ConstantValueRandomizer(TValue value)
        {
            this._value = value;
        }

        public TValue PrepareRandomValue() => this._value;
    }
}