namespace TryAtSoftware.Randomizer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using JetBrains.Annotations;
    using TryAtSoftware.Randomizer.Core.Interfaces;

    public class MembersBinder<TEntity> : IMembersBinder
        where TEntity : class
    {
        private const BindingFlags GET_MEMBERS_BINDING_FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;
        
        public MembersBinder()
        {
            var members = GetMembers(typeof(TEntity));
            this.MemberInfos = new ReadOnlyDictionary<string, MemberInfo>(members);
        }

        public IReadOnlyDictionary<string, MemberInfo> MemberInfos { get; }

        private static Dictionary<string, MemberInfo> GetMembers([NotNull] Type type)
        {
            var membersDict = new Dictionary<string, MemberInfo>();
            var members = GetAllMembers(type).Where(IsValidMember);

            foreach (var member in members)
            {
                // Take only the first member.
                if (membersDict.ContainsKey(member.Name))
                    continue;

                membersDict[member.Name] = member;
            }

            return membersDict;
        }

        private static IEnumerable<MemberInfo> GetAllMembers([NotNull] Type type)
        {
            var interfaces = type.GetInterfaces();
            var typesHierarchy = new List<Type>(interfaces.Length + 1);
            typesHierarchy.AddRange(interfaces);
            typesHierarchy.Add(type);

            return typesHierarchy.SelectMany(t => t.GetMembers(GET_MEMBERS_BINDING_FLAGS)).Distinct();
        }

        private static bool IsValidMember(MemberInfo memberInfo)
        {
            if (memberInfo is null)
                return false;

            return memberInfo switch
            {
                PropertyInfo pi => pi.CanWrite,
                _ => false
            };
        }
    }
}