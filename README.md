[![Quality gate](https://sonarcloud.io/api/project_badges/quality_gate?project=TryAtSoftware_Randomizer)](https://sonarcloud.io/dashboard?id=TryAtSoftware_Randomizer)
[![Core validation](https://github.com/TryAtSoftware/Randomizer/actions/workflows/Core%20validation.yml/badge.svg)](https://github.com/TryAtSoftware/Randomizer/actions/workflows/Core%20validation.yml)

# About the project

`TryAtSoftware.Randomizer` is a library that should simplify the process of random values generation. no matter of the complexity.
We offer a set of methods and components that can be used to generate random values of different types. They are reusable and can be applied to every projects of yours if you wish.

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

