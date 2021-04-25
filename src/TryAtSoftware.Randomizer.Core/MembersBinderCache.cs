namespace TryAtSoftware.Randomizer.Core
{
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public static class MembersBinderCache<TEntity>
        where TEntity : class
    {
        static MembersBinderCache()
        {
            Binder = new MembersBinder<TEntity>();
        }

        // NOTE: Tony Troeff, 18/04/2021 - This is the idea of this class - to provide a single `IMembersBinder` instance for any requested type represented by the generic parameter.
        // ReSharper disable once StaticMemberInGenericType
        public static IMembersBinder<TEntity> Binder { get; }
    }
}