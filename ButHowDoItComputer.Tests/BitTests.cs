using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class BitTests
    {
        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void ConstructorSetsInitialState(bool expected)
        {
            Assert.AreEqual(expected, new Bit(expected).State);
        }
    }
}