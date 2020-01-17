using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class IsZeroGateTests
    {
        private IsZeroGate _sut;
        private ByteToBase10Converter _base10ToByte;

        [SetUp]
        public void Setup()
        {
            _sut = new IsZeroGate(TestUtils.CreateOr(), TestUtils.CreateNot());
            _base10ToByte = TestUtils.CreateByteToBase10Converter();
        }

        [Test]
        [TestCase(0U, true)]
        [TestCase(1U, false)]
        public void CanCheckByteIsZero(uint tVal, bool expected)
        {
            var testValue = _base10ToByte.ToByte(tVal);
            var actual = _sut.IsZero(testValue);
            Assert.AreEqual(expected, actual.State);
        }
    }
}