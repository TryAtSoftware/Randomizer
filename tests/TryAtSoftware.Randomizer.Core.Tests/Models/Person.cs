namespace TryAtSoftware.Randomizer.Core.Tests.Models
{
    using System;
    using JetBrains.Annotations;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Person : IEquatable<Person>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsEmployed { get; set; }
        public int Age { get; set; }

        public bool Equals(Person other)
        {
            if (other is null)
                return false;
            
            return this.Id.Equals(other.Id)
                && string.Equals(this.Name, other.Name, StringComparison.Ordinal)
                && this.IsEmployed == other.IsEmployed
                && this.Age == other.Age;
        }
    }
}