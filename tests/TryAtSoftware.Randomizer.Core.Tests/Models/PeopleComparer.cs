namespace TryAtSoftware.Randomizer.Core.Tests.Models
{
    using System;
    using System.Collections.Generic;

    public class PeopleComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null))
                return false;
            if (ReferenceEquals(y, null))
                return false;
            if (x.GetType() != y.GetType())
                return false;

            return x.Id.Equals(y.Id) && string.Equals(x.Name, y.Name, StringComparison.Ordinal) && x.IsEmployed == y.IsEmployed && x.Age == y.Age;
        }

        public int GetHashCode(Person person) => HashCode.Combine(person.Id, person.Name, person.IsEmployed, person.Age);
    }
}