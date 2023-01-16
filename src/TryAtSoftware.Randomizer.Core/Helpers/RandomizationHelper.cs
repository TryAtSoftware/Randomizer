namespace TryAtSoftware.Randomizer.Core.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public static class RandomizationHelper
{
    public const string LOWER_CASE_LETTERS = "abcdefghijklmnopqrstuvwxyz";
    public const string UPPER_CASE_LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string DIGITS = "0123456789";

    public static int RandomInteger(int inclusiveBottomBound, int exclusiveUpperBound)
    {
        if (exclusiveUpperBound <= inclusiveBottomBound) throw new InvalidOperationException("The maximum value for the random number that should be generated cannot be lower than or equal to the minimum.");
        if (exclusiveUpperBound == inclusiveBottomBound + 1) return inclusiveBottomBound;

        using var rng = new RNGCryptoServiceProvider();
        var data = new byte[4];
        rng.GetBytes(data);

        var generatedValue = BitConverter.ToInt32(data, startIndex: 0);

        var difference = exclusiveUpperBound - inclusiveBottomBound;
        var differenceOffset = Math.Abs(generatedValue) % difference;
        return inclusiveBottomBound + differenceOffset;
    }

    public static string GetRandomString()
    {
        var charactersMask = $"{LOWER_CASE_LETTERS}{UPPER_CASE_LETTERS}{DIGITS}";
        return GetRandomString(RandomInteger(30, 80), charactersMask);
    }

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

    public static DateTimeOffset GetRandomDate(bool historical = false)
    {
        var randomRepetitionsCount = RandomInteger(3, 6);
        long ticks = 1;
        for (var i = 0; i < randomRepetitionsCount; i++)
            ticks *= RandomInteger(10, 1000);

        var randTimeSpan = new TimeSpan(ticks);
        return historical ? DateTimeOffset.Now.Add(-randTimeSpan) : DateTimeOffset.Now.Add(randTimeSpan);
    }
}