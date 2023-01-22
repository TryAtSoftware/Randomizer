namespace TryAtSoftware.Randomizer.Core.Tests;

using System.Collections.Generic;
using TryAtSoftware.Randomizer.Core.Helpers;

public static class TestsHelper
{
    public static IEnumerable<object[]> GetInvalidStringParameters()
    {
        yield return new object[] { null! };
        yield return new object[] { "" };
        yield return new object[] { "   " };
        yield return new object[] { "\t" };
        yield return new object[] { "\r" };
        yield return new object[] { "\n" };
        yield return new object[] { "\t\r\n" };
    }

    public static void Shuffle<T>(T[] array, int iterations = 20)
    {
        for (var i = 0; i < iterations; i++)
        {
            var a = RandomizationHelper.RandomInteger(0, array.Length);
            var b = RandomizationHelper.RandomInteger(0, array.Length);

            (array[a], array[b]) = (array[b], array[a]);
        }
    }
}