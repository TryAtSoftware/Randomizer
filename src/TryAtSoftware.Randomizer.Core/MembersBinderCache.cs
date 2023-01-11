namespace TryAtSoftware.Randomizer.Core
{
    using System.Reflection;
    using TryAtSoftware.Extensions.Reflection;
    using TryAtSoftware.Extensions.Reflection.Interfaces;

    public static class MembersBinderCache<TEntity>
        where TEntity : class
    {
        static MembersBinderCache()
        {
            Binder = new MembersBinder<TEntity>(IsValidMember, BindingFlags.Public | BindingFlags.Instance);
        }

        // NOTE: Tony Troeff, 18/04/2021 - This is the idea of this class - to provide a single `IMembersBinder` instance for any requested type represented by the generic parameter.
        // ReSharper disable once StaticMemberInGenericType
        public static IMembersBinder Binder { get; }

        private static bool IsValidMember(MemberInfo memberInfo)
            => memberInfo switch
            {
                PropertyInfo pi => pi.CanWrite,
                FieldInfo _ => true,
                _ => false
            };
    }
}