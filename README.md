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

## Randomizing primitive values

Most of the necessary methods for generating primitive values are contained within the `RandomizationHelper` class.
In the next sections you can read more about every one of them.

### Generating random integers

There are multiple methods that can be used to generate random integers:
- `RandomInteger` generates a random 32-bit signed integer within a given range.
- `RandomUnsignedInteger` generates a random 32-bit unsigned integer within a given range.
- `RandomLongInteger` generates a random 64-bit signed integer within a given range.
- `RandomUnsignedLongInteger` generates a random 64-bit unsigned integer within a given range.

#### Use cases

##### Without range restrictions

If no parameters are provided, no range constraints will be applied when generating the random integer.

```C#
// The next line will generate a random 32-bit signed integer in range: [int.MinValue, int.MaxValue]
int randomInteger = RandomizationHelper.RandomInteger();
```

##### With range restrictions

If two numbers are provided (inclusive lower bound and exclusive upper bound), the generated integer will be in the `[inclusive_lower_bound, exclusive_upper_bound)` range.
_As the provided upper bound is exclusive, it will equal one more than the greatest value that can be generated._

```C#
// The next line will generate a random 64-bit unsigned integer in range: [0, 100)
// NOTE: The upper bound is exclusive, so the maximum value that can be generated is 100 - 1 = 99.
ulong randomInteger = RandomizationHelper.RandomUnsignedLongInteger(0UL, 100UL);
```

This overload is very convenient whenever a random index should be generated.

```C#
int[] array = new int[100];
for (int i = 0; i < array.Length; i++) array[i] = i;

var randomIndex = RandomizationHelper.RandomInteger(0, array.Length);
```

If the upper bound should be inclusive, we suggest using the third overload - it accepts an additional boolean value defining whether or not the upper bound should be interpreted as exclusive or inclusive.

```C#
// NOTE: All statements in the following example are equivalent!

// The next line will generate a random 32-bit unsigned integer in range: [1, 1000]
// NOTE: The upper bound is inclusive, so it equals the maximum value that can be generated.
long randomInteger1 = RandomizationHelper.RandomLongInteger(1, 1000, upperBoundIsExclusive: false);
long randomInteger2 = RandomizationHelper.RandomLongInteger(1, 1001, upperBoundIsExclusive: true);
long randomInteger2 = RandomizationHelper.RandomLongInteger(1, 1001);
```
> If only one of the bounds is known, you can use `T.MinValue` or `T.MaxValue` as fillers (where `T` is the corresponding numeric type).

#### Inclusive maximum values

Most of the existing methods for generating random numerical values do not include `T.MaxValue` (where `T` is the corresponding numeric type).
We have noticed that this limitation is completely unnecessary and it is isolated from our code base.

```C#
// The next line will generate a random 64-bit signed integer in range: [0, long.MaxValue]
long randomInteger = RandomizationHelper.RandomLongInteger(0, long.MaxValue, upperBoundIsExclusive: false);
```

> We cannot use the logic from the previous section as `T.MaxValue + 1` will cause an overflow.

### Generating random floating-point numbers

There are two methods that can be used - `RandomDouble` and `RandomFloat`.
They both have the same characteristics and will generate a random floating-point value of the corresponding type within the range `[0, 1)`.

```C#
double randomDouble = RandomizationHelper.RandomDouble();
float randomFloat = RandomizationHelper.RandomFloat();
```

### Generating array of random bytes

The `RandomBytes` method can be very useful when we work with byte arrays, streams, files. etc.
It will generate an array of random byte values by a given length.

```C#
// When working with files:
byte[] fileContent = RandomizationHelper.RandomBytes(length: 1024);
await File.WriteAllBytesAsync("path/to/file", fileContent, cancellationToken);

// When working with streams:
byte[] streamContent = RandomizationHelper.RandomBytes(length: 1024);
await using MemoryStream stream = new MemoryStream(streamContent);
await UploadContentAsync(stream, cancellationToken);
```

### Generating random boolean values

It is quite often necessary to have a mechanism of generating a random boolean value.
The method we can use in this case is called `RandomProbability`.
For example, when making random changes to some elements of a given array - for each index we can generate a random `bool` denoting if a change should be made.

```C#
int[] array = new int[100];
for (int i = 0; i < array.Length; i++)
{
    if (RandomizationHelper.RandomProbability()) array[i] = RandomizationHelper.RandomInteger();
    else array[i] = i;
}
```

This method accepts an optional parameter called `percents`.
It can be used to specify the likelihood of the generated value being `true`.

```C#
// The generated value will be `true` in 1% of the cases (respectively `false` in the other 99%).
bool rarelyTrue = RandomizationHelper.RandomProbability(percents: 1);

// The generated value will be `true` in 99% of the cases (respectively `false` in the other 1%).
bool rarelyFalse = RandomizationHelper.RandomProbability(percents: 99);
```

### Generating random `string` values

TODO

### Generating random `DateTimeOffset` values

TODO

## Custom randomizers

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