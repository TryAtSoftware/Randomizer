namespace TryAtSoftware.Randomizer.Core.Tests.PrimitiveRandomization
{
    using System;
    using TryAtSoftware.Randomizer.Core.Helpers;
    using Xunit;

    public class RandomizationHelperTests
    {
        [Fact]
        public void GetRandomstringShouldValidateLength()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => RandomizationHelper.GetRandomString(-1, "mask"));
        }
        
        [Theory]
        [MemberData(nameof(TestsHelper.GetInvalidStringParameters), MemberType = typeof(TestsHelper))]
        public void GetRandomstringShouldValidateMask(string mask)
        {
            Assert.Throws<ArgumentNullException>(() => RandomizationHelper.GetRandomString(5, mask));
        }
    }
}