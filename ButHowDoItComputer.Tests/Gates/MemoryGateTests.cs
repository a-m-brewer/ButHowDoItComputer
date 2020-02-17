using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Gates
{
    [TestFixture]
    public class MemoryGateTests
    {
        [Test]
        public void CanKeepTheValueOfABit()
        {
            var sut = new MemoryGate(new NAnd(new Not(), new And()));

            // set the initial state of the memory to on
            var result = sut.Apply(true, true);

            // assert that the bit is set
            Assert.AreEqual(true, result);

            // turn off set mode and try and set the input bit to off
            result = sut.Apply(false, false);
            Assert.AreEqual(true, result);
        }

        // this test might be a bit pointless but it gives me piece of mind
        [Test]
        public void CanSetTheBitAgain()
        {
            var sut = new MemoryGate(new NAnd(new Not(), new And()));

            // set the initial state of the memory to on
            var result = sut.Apply(true, true);

            // assert that the bit is set
            Assert.AreEqual(true, result);

            // turn off set mode and try and set the input bit to off
            result = sut.Apply(false, false);

            Assert.AreEqual(true, result);
            result = sut.Apply(false, true);
            Assert.AreEqual(false, result);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void CanStoreABit(bool expected)
        {
            var sut = new MemoryGate(new NAnd(new Not(), new And()));

            var result = sut.Apply(expected, true);

            Assert.AreEqual(expected, result);
        }
    }
}