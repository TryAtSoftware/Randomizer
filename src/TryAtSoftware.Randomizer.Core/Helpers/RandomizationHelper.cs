namespace TryAtSoftware.Randomizer.Core.Helpers;

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// A static class containing helper methods for randomizing primitives.
/// </summary>
public static class RandomizationHelper
{
    private const double RANDOM_DOUBLE_CONSTANT = 1.0 / (1UL << 53);
    private const float RANDOM_SINGLE_CONSTANT = 1.0f / (1UL << 24);

    /// <summary>
    /// All letters from the Latin alphabet in lower case.
    /// </summary>
    public const string LOWER_CASE_LETTERS = "abcdefghijklmnopqrstuvwxyz";
    
    /// <summary>
    /// All letters from the Latin alphabet in upper case.
    /// </summary>
    public const string UPPER_CASE_LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    /// <summary>
    /// All digits.
    /// </summary>
    public const string DIGITS = "0123456789";
    
    /// <summary>
    /// All letters from the Latin alphabet in lower and upper case and all digits.
    /// </summary>
    public const string ALL_CHARACTERS = $"{LOWER_CASE_LETTERS}{UPPER_CASE_LETTERS}{DIGITS}";

    /// <summary>
    /// Generates a random 32-bit integer in the range [Int32.MinValue, Int32.MaxValue].
    /// </summary>
    public static int RandomInteger() => RandomInteger(int.MinValue, int.MaxValue, upperBoundIsExclusive: false);

    /// <summary>
    /// Generates a random 32-bit integer in the range [<paramref name="inclusiveLowerBound"/>, <paramref name="exclusiveUpperBound"/>).
    /// </summary>
    /// <param name="inclusiveLowerBound">The lowest value that can be generated. This parameter is inclusive.</param>
    /// <param name="exclusiveUpperBound">One more than the greatest value that can be generated. This parameter is exclusive.</param>
    public static int RandomInteger(int inclusiveLowerBound, int exclusiveUpperBound) => RandomInteger(inclusiveLowerBound, exclusiveUpperBound, upperBoundIsExclusive: true);

    /// <summary>
    /// Generates a random 32-bit integer in the range [<paramref name="inclusiveLowerBound"/>, <paramref name="upperBound"/>) if <paramref name="upperBoundIsExclusive"/> is <c>true</c>.
    /// Generates a random 32-bit integer in the range [<paramref name="inclusiveLowerBound"/>, <paramref name="upperBound"/>] if <paramref name="upperBoundIsExclusive"/> is <c>false</c>.
    /// </summary>
    /// <param name="inclusiveLowerBound">The lowest value that can be generated. This parameter is inclusive.</param>
    /// <param name="upperBound">
    /// One more than the greatest value that can be generated if <paramref name="upperBoundIsExclusive"/> is <c>true</c>.
    /// The greatest value that can be generated if <paramref name="upperBoundIsExclusive"/> is <c>false</c>.
    /// Whether this parameter is inclusive or exclusive, depends on <paramref name="upperBoundIsExclusive"/>.</param>
    /// <param name="upperBoundIsExclusive">A value indicating whether the <paramref name="upperBound"/> is an inclusive or an exclusive parameter.</param>
    public static int RandomInteger(int inclusiveLowerBound, int upperBound, bool upperBoundIsExclusive) => int.MinValue + (int)RandomUnsignedInteger((uint)(inclusiveLowerBound - int.MinValue), (uint)(upperBound - int.MinValue), upperBoundIsExclusive);

    /// <summary>
    /// Generates a random 32-bit unsigned integer in the range [UInt32.MinValue, UInt32.MaxValue].
    /// </summary>
    public static uint RandomUnsignedInteger() => RandomUnsignedInteger(uint.MinValue, uint.MaxValue, upperBoundIsExclusive: false);

    /// <summary>
    /// Generates a random 32-bit unsigned integer in the range [<paramref name="inclusiveLowerBound"/>, <paramref name="exclusiveUpperBound"/>).
    /// </summary>
    /// <param name="inclusiveLowerBound">The lowest value that can be generated. This parameter is inclusive.</param>
    /// <param name="exclusiveUpperBound">One more than the greatest value that can be generated. This parameter is exclusive.</param>
    public static uint RandomUnsignedInteger(uint inclusiveLowerBound, uint exclusiveUpperBound) => RandomUnsignedInteger(inclusiveLowerBound, exclusiveUpperBound, upperBoundIsExclusive: true);

    /// <summary>
    /// Generates a random 32-bit unsigned integer in the range [<paramref name="inclusiveLowerBound"/>, <paramref name="upperBound"/>) if <paramref name="upperBoundIsExclusive"/> is <c>true</c>.
    /// Generates a random 32-bit unsigned integer in the range [<paramref name="inclusiveLowerBound"/>, <paramref name="upperBound"/>] if <paramref name="upperBoundIsExclusive"/> is <c>false</c>.
    /// </summary>
    /// <param name="inclusiveLowerBound">The lowest value that can be generated. This parameter is inclusive.</param>
    /// <param name="upperBound">
    /// One more than the greatest value that can be generated if <paramref name="upperBoundIsExclusive"/> is <c>true</c>.
    /// The greatest value that can be generated if <paramref name="upperBoundIsExclusive"/> is <c>false</c>.
    /// Whether this parameter is inclusive or exclusive, depends on <paramref name="upperBoundIsExclusive"/>.</param>
    /// <param name="upperBoundIsExclusive">A value indicating whether the <paramref name="upperBound"/> is an inclusive or an exclusive parameter.</param>
    public static uint RandomUnsignedInteger(uint inclusiveLowerBound, uint upperBound, bool upperBoundIsExclusive) => (uint) RandomUnsignedLongInteger(inclusiveLowerBound, upperBound, upperBoundIsExclusive);

    /// <summary>
    /// Generates a random 64-bit integer in the range [Int64.MinValue, Int64.MaxValue].
    /// </summary>
    public static long RandomLongInteger() => RandomLongInteger(long.MinValue, long.MaxValue, upperBoundIsExclusive: false);

    /// <summary>
    /// Generates a random 64-bit integer in the range [<paramref name="inclusiveLowerBound"/>, <paramref name="exclusiveUpperBound"/>).
    /// </summary>
    /// <param name="inclusiveLowerBound">The lowest value that can be generated. This parameter is inclusive.</param>
    /// <param name="exclusiveUpperBound">One more than the greatest value that can be generated. This parameter is exclusive.</param>
    public static long RandomLongInteger(long inclusiveLowerBound, long exclusiveUpperBound) => RandomLongInteger(inclusiveLowerBound, exclusiveUpperBound, upperBoundIsExclusive: true);

    /// <summary>
    /// Generates a random 64-bit integer in the range [<paramref name="inclusiveLowerBound"/>, <paramref name="upperBound"/>) if <paramref name="upperBoundIsExclusive"/> is <c>true</c>.
    /// Generates a random 64-bit integer in the range [<paramref name="inclusiveLowerBound"/>, <paramref name="upperBound"/>] if <paramref name="upperBoundIsExclusive"/> is <c>false</c>.
    /// </summary>
    /// <param name="inclusiveLowerBound">The lowest value that can be generated. This parameter is inclusive.</param>
    /// <param name="upperBound">
    /// One more than the greatest value that can be generated if <paramref name="upperBoundIsExclusive"/> is <c>true</c>.
    /// The greatest value that can be generated if <paramref name="upperBoundIsExclusive"/> is <c>false</c>.
    /// Whether this parameter is inclusive or exclusive, depends on <paramref name="upperBoundIsExclusive"/>.</param>
    /// <param name="upperBoundIsExclusive">A value indicating whether the <paramref name="upperBound"/> is an inclusive or an exclusive parameter.</param>
    public static long RandomLongInteger(long inclusiveLowerBound, long upperBound, bool upperBoundIsExclusive) => long.MinValue + (long)RandomUnsignedLongInteger((ulong)(inclusiveLowerBound - long.MinValue), (ulong)(upperBound - long.MinValue), upperBoundIsExclusive);

    /// <summary>
    /// Generates a random 64-bit unsigned integer in the range [Int64.MinValue, Int64.MaxValue].
    /// </summary>
    public static ulong RandomUnsignedLongInteger() => RandomUnsignedLongInteger(ulong.MinValue, ulong.MaxValue, upperBoundIsExclusive: false);

    /// <summary>
    /// Generates a random 64-bit unsigned integer in the range [<paramref name="inclusiveLowerBound"/>, <paramref name="exclusiveUpperBound"/>).
    /// </summary>
    /// <param name="inclusiveLowerBound">The lowest value that can be generated. This parameter is inclusive.</param>
    /// <param name="exclusiveUpperBound">One more than the greatest value that can be generated. This parameter is exclusive.</param>
    public static ulong RandomUnsignedLongInteger(ulong inclusiveLowerBound, ulong exclusiveUpperBound) => RandomUnsignedLongInteger(inclusiveLowerBound, exclusiveUpperBound, upperBoundIsExclusive: true);

    /// <summary>
    /// Generates a random 64-bit unsigned integer in the range [<paramref name="inclusiveLowerBound"/>, <paramref name="upperBound"/>) if <paramref name="upperBoundIsExclusive"/> is <c>true</c>.
    /// Generates a random 64-bit unsigned integer in the range [<paramref name="inclusiveLowerBound"/>, <paramref name="upperBound"/>] if <paramref name="upperBoundIsExclusive"/> is <c>false</c>.
    /// </summary>
    /// <param name="inclusiveLowerBound">The lowest value that can be generated. This parameter is inclusive.</param>
    /// <param name="upperBound">
    /// One more than the greatest value that can be generated if <paramref name="upperBoundIsExclusive"/> is <c>true</c>.
    /// The greatest value that can be generated if <paramref name="upperBoundIsExclusive"/> is <c>false</c>.
    /// Whether this parameter is inclusive or exclusive, depends on <paramref name="upperBoundIsExclusive"/>.</param>
    /// <param name="upperBoundIsExclusive">A value indicating whether the <paramref name="upperBound"/> is an inclusive or an exclusive parameter.</param>
    public static ulong RandomUnsignedLongInteger(ulong inclusiveLowerBound, ulong upperBound, bool upperBoundIsExclusive)
    {
        if (upperBound < inclusiveLowerBound || (upperBoundIsExclusive && upperBound == inclusiveLowerBound)) throw new InvalidOperationException(GetInvalidRangeExceptionMessage(upperBoundIsExclusive));
        
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

    /// <summary>
    /// Generates a random floating-point number of type <see cref="double"/> in the range [0, 1).
    /// </summary>
    public static double RandomDouble() => (RandomUInt64() >> 11) * RANDOM_DOUBLE_CONSTANT;
    
    /// <summary>
    /// Generates a random floating-point number of type <see cref="float"/> in the range [0, 1).
    /// </summary>
    public static float RandomFloat() => (RandomUInt64() >> 40) * RANDOM_SINGLE_CONSTANT;

    /// <summary>
    /// Generates an array filled with random bytes.
    /// </summary>
    /// <param name="length">The length of the generated array of bytes.</param>
    public static byte[] RandomBytes(int length)
    {
        var result = new byte[length];
        RandomNumberGenerator.Fill(result);
        return result;
    }

    /// <summary>
    /// Generates a random string with random length using the <see cref="ALL_CHARACTERS"/> mask.
    /// </summary>
    public static string GetRandomString() => GetRandomString(RandomInteger(30, 80, upperBoundIsExclusive: false), ALL_CHARACTERS);

    /// <summary>
    /// Generates a random string by a given length and characters mask.
    /// </summary>
    /// <param name="length">The length of the string that should be generated.</param>
    /// <param name="charactersMask">All characters that can be used when generating the string.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown, if the provided <paramref name="length"/> is lower than or equal to zero.</exception>
    /// <exception cref="ArgumentNullException">Thrown if the provided <paramref name="charactersMask"/> is null or empty.</exception>
    public static string GetRandomString(int length, string charactersMask)
    {
        if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));
        if (string.IsNullOrEmpty(charactersMask)) throw new ArgumentNullException(nameof(charactersMask));

        // Returns a random string with given length, it uses the characters above.
        return GetRandomString(length, charactersMask.ToCharArray());
    }

    /// <summary>
    /// Generates a random string by a given length and characters mask.
    /// </summary>
    /// <param name="length">The length of the string that should be generated.</param>
    /// <param name="charactersMask">All characters that can be used when generating the string.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown, if the provided <paramref name="length"/> is lower than or equal to zero.</exception>
    /// <exception cref="ArgumentNullException">Thrown if the provided <paramref name="charactersMask"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the provided <paramref name="charactersMask"/> is empty.</exception>
    public static string GetRandomString(int length, IReadOnlyList<char> charactersMask)
    {
        if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));
        if (charactersMask is null) throw new ArgumentNullException(nameof(charactersMask));
        if (charactersMask.Count == 0) throw new ArgumentException("The characters mask must include at least one character.", nameof(charactersMask));

        var sb = new StringBuilder(length);

        for (var i = 0; i < length; i++)
            sb.Append(charactersMask[RandomInteger(0, charactersMask.Count)]);

        return sb.ToString();
    }

    /// <summary>
    /// Generates a random boolean.
    /// </summary>
    /// <param name="percents">The likelihood (out of 100) for this method to return <c>true</c>.</param>
    public static bool RandomProbability(int percents = 50) => RandomInteger(0, 100) < percents;

    /// <summary>
    /// Generates a random <see cref="DateTimeOffset"/> value.
    /// </summary>
    /// <param name="historical">Value indicating whether the generated value should be in the past (if <c>true</c>) or in the future (if <c>false</c>).</param>
    public static DateTimeOffset GetRandomDateTimeOffset(bool historical = false)
    {
        var ticks = RandomLongInteger(0, 100000);
        var randTimeSpan = new TimeSpan(ticks);
        return historical ? DateTimeOffset.Now.Add(-randTimeSpan) : DateTimeOffset.Now.Add(randTimeSpan);
    }

    private static ulong RandomUInt64() => BitConverter.ToUInt64(RandomBytes(8));

    private static string GetInvalidRangeExceptionMessage(bool upperBoundIsExclusive)
    {
        if (upperBoundIsExclusive) return "The exclusive upper bound cannot be lower than or equal to the lower bound."; 
        return "The inclusive upper bound cannot be lower than the lower bound.";
    }
}