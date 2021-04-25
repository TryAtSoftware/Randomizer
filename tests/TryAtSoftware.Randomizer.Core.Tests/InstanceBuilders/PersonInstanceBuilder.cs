namespace TryAtSoftware.Randomizer.Core.Tests.InstanceBuilders
{
    using TryAtSoftware.Randomizer.Core.Interfaces;
    using TryAtSoftware.Randomizer.Core.Tests.Models;

    public class PersonInstanceBuilder : IInstanceBuilder<Person>
    {
        public Person PrepareNewInstance() => new Person();
    }
}