namespace TryAtSoftware.Randomizer.Core.Tests;

using System.Collections.Generic;

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
}