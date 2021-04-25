namespace TryAtSoftware.Randomizer.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using JetBrains.Annotations;

    public static class RandomizationHelper
    {
        public const string LOWER_CASE_LETTERS = "abcdefghijklmnopqrstuvwxyz";
        public const string UPPER_CASE_LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string DIGITS = "0123456789";

        public static int GetRandomNumber(int exclusiveUpperBound) => RandomNumberGenerator.GetInt32(exclusiveUpperBound);
        public static int GetRandomNumber(int inclusiveBottomBound, int exclusiveUpperBound) => RandomNumberGenerator.GetInt32(inclusiveBottomBound, exclusiveUpperBound);

        public static string RandomString()
        {
            var charactersMask = $"{LOWER_CASE_LETTERS}{UPPER_CASE_LETTERS}{DIGITS}";
            return RandomString(GetRandomNumber(30, 80), charactersMask);
        }

        public static string RandomString(int length, [NotNull] string charactersMask)
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
                sb.Append(possibleChars[GetRandomNumber(possibleChars.Count)]);

            return sb.ToString();
        }

        public static bool RandomProbability(int percents = 50) => RandomNumberGenerator.GetInt32(100) < percents;

        public static DateTimeOffset GetRandomDate(bool historical = false)
        {
            var randomRepetitionsCount = GetRandomNumber(3, 6);
            long ticks = 1;
            for (var i = 0; i < randomRepetitionsCount; i++)
                ticks *= GetRandomNumber(10, 1000);
            
            var randTimeSpan = new TimeSpan(ticks);
            return historical ? DateTimeOffset.Now.Add(-randTimeSpan) : DateTimeOffset.Now.Add(randTimeSpan);
        }
    }
}