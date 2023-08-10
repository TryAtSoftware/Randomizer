namespace TryAtSoftware.Randomizer.Core.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class RandomizationHelper
{
    public const string LOWER_CASE_LETTERS = "abcdefghijklmnopqrstuvwxyz";
    public const string UPPER_CASE_LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string DIGITS = "0123456789";
    public const string ALL_CHARACTERS = $"{LOWER_CASE_LETTERS}{UPPER_CASE_LETTERS}{DIGITS}";

    private static Random _random = new ();

    public static int RandomInteger() => RandomInteger(0, int.MaxValue);

    public static int RandomInteger(int inclusiveBottomBound, int exclusiveUpperBound)
    {
        if (exclusiveUpperBound <= inclusiveBottomBound) throw new InvalidOperationException("The maximum value for the random number that should be generated cannot be lower than or equal to the minimum.");

        var range = (uint)(exclusiveUpperBound - inclusiveBottomBound);
        var limit = uint.MaxValue - uint.MaxValue % range;

        uint result;
        do
        {
            result = RandomUInt32();
        } while (result > limit);

        return inclusiveBottomBound + (int)(result % range);
    }

    public static long RandomLongInteger() => RandomLongInteger(0, long.MaxValue);

    public static long RandomLongInteger(long inclusiveBottomBound, long exclusiveUpperBound)
    {
        if (exclusiveUpperBound <= inclusiveBottomBound) throw new InvalidOperationException("The maximum value for the random number that should be generated cannot be lower than or equal to the minimum.");

        var range = (ulong)(exclusiveUpperBound - inclusiveBottomBound);
        var limit = ulong.MaxValue - ulong.MaxValue % range;

        ulong result;
        do
        {
            result = RandomUInt64();
        } while (result > limit);

        return inclusiveBottomBound + (long)(result % range);
    }

    public static string GetRandomString() => GetRandomString(RandomInteger(30, 80), ALL_CHARACTERS);

    public static string GetRandomString(int length, string charactersMask)
    {
        if (length <= 0)
            throw new ArgumentOutOfRangeException(nameof(length));
        if (string.IsNullOrWhiteSpace(charactersMask))
            throw new ArgumentNullException(nameof(charactersMask));

        // Returns a random string with given length, it uses the characters above.
        return GetRandomStringCombination(length, charactersMask.ToList().AsReadOnly());
    }

    public static string GetRandomStringCombination(int length, IReadOnlyList<char> possibleChars)
    {
        if (possibleChars is null)
            throw new ArgumentNullException(nameof(possibleChars));
        if (length <= 0)
            throw new ArgumentOutOfRangeException(nameof(length));

        var sb = new StringBuilder(length);

        for (var i = 0; i < length; i++)
            sb.Append(possibleChars[RandomInteger(0, possibleChars.Count)]);

        return sb.ToString();
    }

    public static bool RandomProbability(int percents = 50) => RandomInteger(0, 100) < percents;

    public static DateTimeOffset GetRandomDateTimeOffset(bool historical = false)
    {
        var ticks = RandomLongInteger(0, long.MaxValue);
        var randTimeSpan = new TimeSpan(ticks);
        return historical ? DateTimeOffset.Now.Add(-randTimeSpan) : DateTimeOffset.Now.Add(randTimeSpan);
    }

    private static uint RandomUInt32()
    {
        var buffer = new byte[4];
        _random.NextBytes(buffer);
        return BitConverter.ToUInt32(buffer);
    }

    private static ulong RandomUInt64()
    {
        var buffer = new byte[8];
        _random.NextBytes(buffer);
        return BitConverter.ToUInt64(buffer);
    }
}