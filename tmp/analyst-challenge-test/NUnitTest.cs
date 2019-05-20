using NUnit.Framework;

namespace analyst_challenge_test
{
    [TestFixture]
    public class NUnitTest
    {
        [TestCase()]
        public void Test1()
        {
            Assert.Equals(1, 1);
            Assert.Equals(1, 2);
            Assert.Pass();
        }
    }
}
