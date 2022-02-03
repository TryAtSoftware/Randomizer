namespace TryAtSoftware.Randomizer.Core.Tests.Models
{
    public class Car
    {
        public Car(string make, string model, int year)
        {
            this.Make = make;
            this.Model = model;
            this.Year = year;
        }

        public string Make { get;  }
        public string Model { get; }
        public int Year { get; }
    }
}