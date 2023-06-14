[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Randomizer&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Randomizer)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Randomizer&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Randomizer)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Randomizer&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Randomizer)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Randomizer&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Randomizer)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Randomizer&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Randomizer)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=TryAtSoftware_Randomizer&metric=bugs)](https://sonarcloud.io/summary/new_code?id=TryAtSoftware_Randomizer)

[![Core validation](https://github.com/TryAtSoftware/Randomizer/actions/workflows/Core%20validation.yml/badge.svg)](https://github.com/TryAtSoftware/Randomizer/actions/workflows/Core%20validation.yml)

# About the project

`TryAtSoftware.Randomizer` is a library that should simplify the process of random values generation.
We offer a set of methods and components that can be used to generate random values of different types. They are reusable and can be applied to every projects of yours.

# About us

`Try At Software` is a software development company based in Bulgaria. We are mainly using `dotnet` technologies (`C#`, `ASP.NET Core`, `Entity Framework Core`, etc.) and our main idea is to provide a set of tools that can simplify the majority of work a developer does on a daily basis.

# Getting started

## Installing the package

Before creating any randomizers, you need to install the package.
The simplest way to do this is to either use the `NuGet package manager`, or the `dotnet CLI`.

Using the `NuGet package manager` console within Visual Studio, you can install the package using the following command:
> Install-Package TryAtSoftware.Randomizer

Or using the `dotnet CLI` from a terminal window:
> dotnet add package TryAtSoftware.Randomizer

## Creating your first randomizer

To define a randomizer, you need to create a class that implements the `IRandomizer<T>` interface, where `T` is the type of information this component should be responsible for randomizing.
For example, imagine that you want to create a randomizer that generates a random `DateTime` instance in the future.
You would need the following class:

```C#
using System;
using TryAtSoftware.Randomizer.Core.Helpers;
using TryAtSoftware.Randomizer.Core.Interfaces;

public class DateTimeRandomizer : IRandomizer<DateTime>
{
    private readonly DateTime _instantiationTime = DateTime.Now;

    public DateTime PrepareRandomValue()
    {
        // We want to generate a random DateTime instance that is a few minutes ahead of our current time.
        var randomOffsetInMinutes = RandomizationHelper.RandomInteger(15, 60);
        return this._instantiationTime.AddMinutes(randomOffsetInMinutes);
    }
}
```

Then you can use this randomizer as follows:
```C#
var dateTimeRandomizer = new DateTimeRandomizer();
for (int i = 0; i < 5; i++)
{
    var randomDateTime = dateTimeRandomizer.PrepareRandomValue();
    Console.WriteLine(randomDateTime);
}
```

If you run this code, you will see that the output is a few random `DateTime` instances.
You may notice that the output sometimes may contain a value more than once.
This is because the `strength` of our randomizer is not great.
If we analyze it more deeply, we can see that it can generate only 45 different values by design.
We will discuss various mechanisms to increase the `strength` of our randomizers in some of the next sections.

## Randomizing complex types

As you saw in the previous chapter, writing standard randomizers for simple types is easy - you just implement a single method and that's it.
Randomizing complex types is not a more complex operation. We have provided you whit a base class that you can use to implement your own complex randomizers.
It is called `ComplexRandomizer<T>` where `T` is the complex type it should be responsible for randomizing.

Let's start with a basic example! For simplicity, we will work with this model:
```C#
using System;

public class Person
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsEmployed { get; set; }
    public int Age { get; set; }
    public DateTimeOffset EventDate { get; set; }
}
```

Our complex randomizer will look like this:
```C#
using TryAtSoftware.Randomizer.Core;
using TryAtSoftware.Randomizer.Core.Primitives;

public class PersonRandomizer : ComplexRandomizer<Person>
{
    public PersonRandomizer()
    {
        this.Randomize(p => p.Id, new GuidRandomizer());
        this.Randomize(p => p.Name, new StringRandomizer());
        this.Randomize(p => p.Age, new NumberRandomizer());
        this.Randomize(p => p.IsEmployed, new BooleanRandomizer());
        this.Randomize(p => p.EventDate, new DateTimeOffsetRandomizer());
    }
}
```

Let's examine this complex randomizer!

We can see that it can be used to define a specific randomizer for each publicly exposed member of the `Person` type.
And actually this is the main idea of our library. This way we can reuse already defined randomizers for different types.
We can combine them to create complex randomizers that can generate random instances of any type. Isn't that great?!

If we want to examine the complex randomization process more deeply, we can see that it is contained of two phases that we will describe in the next sections.

### Instance building phase

The `IInstanceBuilder<T>` is a component responsible for instantiating the complex type.
By default, a `GeneralInstanceBuilder<T>` will be used if none is specified.
This is the recommended option of instance building as it is implemented to find the most suitable constructor and invoke it (so you do not have to write any additional code).
Furthermore, it tracks down the used parameters so the randomization rules defining them will not be used during the next phase of the complex randomization process.

However, if you want to implement one by yourself, you have a few options:
- You can implement the `IInstanceBuilder<Person>` interface and define some custom instantiation logic. If you chose this option, you are in full control of the instantiation process.
```C#
using TryAtSoftware.Randomizer.Core;
using TryAtSoftware.Randomizer.Core.Interfaces;

public class PersonInstanceBuilder : IInstanceBuilder<Person>
{
    public IInstanceBuildingResult<Person> PrepareNewInstance(IInstanceBuildingArguments arguments)
    {
        // You can use the provided instance building arguments to customize the instance.
        // Note that if you do so, the returned instance building result may need some changes.
        var person = new Person();
        return new InstanceBuildingResult<Person>(person);
    }
}
```

- You may inherit the `SimpleInstanceBuilder<T>` class and implement the `PrepareNewInstance` method.
This option should be used for really simple instance building that does not require any additional parameters being provided to the constructor.
It is most suitable for complex types that have publicly exposed non-read-only members.
```C#
using TryAtSoftware.Randomizer.Core;

public class PersonInstanceBuilder : SimpleInstanceBuilder<Person>
{
    protected override Person PrepareNewInstance() => new Person();
}
```

### Value setting phase

In this phase the `SetValue` method for the value setter of each registered randomization rule will be invoked.
The order will be preserved - the value setter of a given randomization rule will be invoked before the value setter of a randomization rule that is registered after the given.

## Best practices and recommendations

- We do not recommend using `complex randomizers` with abstract classes or interfaces.
While it is possible to make such a setup work, there may be some intricacies along the way.
Another idea that is especially useful when dealing with many derived types that have behavioral but not structural differences, is to make the `complex randomizer` generic.

# Helpful Links

For additional information on troubleshooting, migration guides, answers to Frequently asked questions (FAQ), and more, you can refer to the [Wiki pages](https://github.com/TryAtSoftware/Randomizer/wiki) of this project.