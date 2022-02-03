namespace TryAtSoftware.Randomizer.Core.Tests.InstanceBuilders
{
    using TryAtSoftware.Randomizer.Core.Tests.Models;

    public class PersonInstanceBuilder : SimpleInstanceBuilder<Person>
    {
        protected override Person PrepareNewInstance() => new();
    }
}