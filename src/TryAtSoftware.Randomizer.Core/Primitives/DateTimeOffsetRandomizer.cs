namespace TryAtSoftware.Randomizer.Core.Primitives;

using System;
using TryAtSoftware.Randomizer.Core.Helpers;
using TryAtSoftware.Randomizer.Core.Interfaces;

public class DateTimeOffsetRandomizer : IRandomizer<DateTimeOffset>
{
    public DateTimeOffset PrepareRandomValue() => RandomizationHelper.GetRandomDate(historical: true);
}