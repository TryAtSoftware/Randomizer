namespace TryAtSoftware.Randomizer.Core;

using System.Reflection;
using TryAtSoftware.Extensions.Reflection;
using TryAtSoftware.Extensions.Reflection.Interfaces;

// NOTE: Tony Troeff, 18/04/2021 - This is the idea of this class - to provide a single `IMembersBinder` instance for any requested type represented by the generic parameter.
public class MembersBinderCache<TEntity>
    where TEntity : class
{
    public static MembersBinderCache<TEntity> Instance { get; } = Initialize();
    
    public MembersBinderCache()
    {
        this.Binder = new MembersBinder<TEntity>(x => x is PropertyInfo { CanWrite: true }, BindingFlags.Public | BindingFlags.Instance);
    }

    public IMembersBinder Binder { get; }

    private static MembersBinderCache<TEntity> Initialize() => new ();
}