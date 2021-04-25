namespace TryAtSoftware.Randomizer.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using JetBrains.Annotations;

    public class RandomizationHelper
    {
        public static string LowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
        public static string UpperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string Digits = "0123456789";

        public static Random Random { get; } = new Random();

        public static string RandomString()
        {
            var charactersMask = $"{LowerCaseLetters}{UpperCaseLetters}{Digits}";
            return RandomString(Random.Next(30, 80), charactersMask);
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
                sb.Append(possibleChars[Random.Next(possibleChars.Count)]);

            return sb.ToString();
        }

        public static bool RandomProbability(int percents = 50) => Random.Next(100) < percents;

        public static DateTimeOffset GetRandomDate(bool historical = false)
        {
            var randTimeSpan = new TimeSpan((long) (Random.NextDouble() * DateTimeOffset.Now.Ticks));

            return historical ? DateTimeOffset.Now.Add(-randTimeSpan) : DateTimeOffset.Now.Add(randTimeSpan);
        }
    }
}