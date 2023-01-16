namespace TryAtSoftware.Randomizer.Core.Tests.Models;

using System;

public class Person
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsEmployed { get; set; }
    public int Age { get; set; }
    public DateTimeOffset EventDate { get; set; }
}