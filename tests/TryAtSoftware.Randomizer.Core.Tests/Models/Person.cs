namespace TryAtSoftware.Randomizer.Core.Tests.Models
{
    using System;
    using JetBrains.Annotations;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsEmployed { get; set; }
        public int Age { get; set; }
        public DateTimeOffset EventDate { get; set; }
    }
}