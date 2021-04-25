namespace TryAtSoftware.Randomizer.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Reflection;
    using JetBrains.Annotations;

    public interface IMembersBinder<TEntity>
        where TEntity : class
    {
        [NotNull]
        IReadOnlyDictionary<string, MemberInfo> MemberInfos { get; }
    }
}