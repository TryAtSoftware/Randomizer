namespace TryAtSoftware.Randomizer.Core
{
    using System;
    using System.Reflection;
    using JetBrains.Annotations;
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public class RandomValueSetter<TEntity, TValue> : IRandomValueSetter<TEntity>
        where TEntity : class
    {
        private readonly string _propertyName;
        private readonly IRandomizer<TValue> _randomizer;
        private readonly IMembersBinder<TEntity> _binder;

        public RandomValueSetter([NotNull] string propertyName, [NotNull] IRandomizer<TValue> randomizer, [NotNull] IMembersBinder<TEntity> membersBinder)
        {
            this._propertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            this._randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
            this._binder = membersBinder ?? throw new ArgumentNullException(nameof(membersBinder));
        }

        public void SetValue(TEntity instance)
        {
            if (instance is null || this._binder.MemberInfos.TryGetValue(this._propertyName, out var memberInfo) == false)
                return;

            var value = this._randomizer.PrepareRandomValue();

            try
            {
                if (memberInfo is PropertyInfo propertyInfo)
                    propertyInfo.SetValue(instance, value);
            }
            catch
            {
                // If any exception occurs, we want to swallow it.
            }
        }
    }
}