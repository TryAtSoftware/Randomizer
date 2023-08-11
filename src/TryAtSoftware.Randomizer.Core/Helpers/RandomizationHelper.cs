namespace TryAtSoftware.Randomizer.Core.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class RandomizationHelper
{
    private const double RANDOM_DOUBLE_CONSTANT = 1.0 / (1UL << 53);
    public const string LOWER_CASE_LETTERS = "abcdefghijklmnopqrstuvwxyz";
    public const string UPPER_CASE_LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string DIGITS = "0123456789";
    public const string ALL_CHARACTERS = $"{LOWER_CASE_LETTERS}{UPPER_CASE_LETTERS}{DIGITS}";

    private static Random _random = new ();

    public static int RandomInteger() => RandomInteger(int.MinValue, int.MaxValue, upperBoundIsExclusive: false);

    public static int RandomInteger(int inclusiveLowerBound, int exclusiveUpperBound) => RandomInteger(inclusiveLowerBound, exclusiveUpperBound, upperBoundIsExclusive: true);

    public static int RandomInteger(int inclusiveLowerBound, int upperBound, bool upperBoundIsExclusive) => int.MinValue + (int)RandomUnsignedInteger((uint)(inclusiveLowerBound - int.MinValue), (uint)(upperBound - int.MinValue), upperBoundIsExclusive);

    public static uint RandomUnsignedInteger() => RandomUnsignedInteger(uint.MinValue, uint.MaxValue, upperBoundIsExclusive: false);

    public static uint RandomUnsignedInteger(uint inclusiveLowerBound, uint exclusiveUpperBound) => RandomUnsignedInteger(inclusiveLowerBound, exclusiveUpperBound, upperBoundIsExclusive: true);

    public static uint RandomUnsignedInteger(uint inclusiveLowerBound, uint upperBound, bool upperBoundIsExclusive)
    {
        if (upperBound < inclusiveLowerBound || (upperBoundIsExclusive && upperBound == inclusiveLowerBound)) throw new InvalidOperationException("The upper bound for the random number that should be generated cannot be lower than or equal to the lower bound.");
        
        var range = upperBound - inclusiveLowerBound;
        if (!upperBoundIsExclusive)
        {
            if (range == uint.MaxValue) return RandomUInt32();
            range++;
        }

        var limit = uint.MaxValue - uint.MaxValue % range;

        uint result;
        do
        {
            result = RandomUInt32();
        } while (result > limit);

        return inclusiveLowerBound + result % range;
    }

    public static long RandomLongInteger() => RandomLongInteger(long.MinValue, long.MaxValue, upperBoundIsExclusive: false);

    public static long RandomLongInteger(long inclusiveLowerBound, long exclusiveUpperBound) => RandomLongInteger(inclusiveLowerBound, exclusiveUpperBound, upperBoundIsExclusive: true);

    public static long RandomLongInteger(long inclusiveLowerBound, long upperBound, bool upperBoundIsExclusive) => long.MinValue + (long)RandomUnsignedLongInteger((ulong)(inclusiveLowerBound - long.MinValue), (ulong)(upperBound - long.MinValue), upperBoundIsExclusive);

    public static ulong RandomUnsignedLongInteger() => RandomUnsignedLongInteger(ulong.MinValue, ulong.MaxValue, upperBoundIsExclusive: false);

    public static ulong RandomUnsignedLongInteger(ulong inclusiveLowerBound, ulong exclusiveUpperBound) => RandomUnsignedLongInteger(inclusiveLowerBound, exclusiveUpperBound, upperBoundIsExclusive: true);

    public static ulong RandomUnsignedLongInteger(ulong inclusiveLowerBound, ulong upperBound, bool upperBoundIsExclusive)
    {
        if (upperBound < inclusiveLowerBound || (upperBoundIsExclusive && upperBound == inclusiveLowerBound)) throw new InvalidOperationException("The upper bound for the random number that should be generated cannot be lower than or equal to the lower bound.");
        
        var range = upperBound - inclusiveLowerBound;
        if (!upperBoundIsExclusive)
        {
            if (range == ulong.MaxValue) return RandomUInt64();
            range++;
        }

        var limit = ulong.MaxValue - ulong.MaxValue % range;

        ulong result;
        do
        {
            result = RandomUInt64();
        } while (result > limit);

        return inclusiveLowerBound + result % range;
    }

    public static double RandomDouble() => (RandomUInt64() >> 11) * RANDOM_DOUBLE_CONSTANT;

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
        var ticks = RandomLongInteger(0, 100000);
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