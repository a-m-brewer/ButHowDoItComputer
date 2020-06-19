using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Gates
{
    [TestFixture]
    public class BitAdderTests
    {
        public BitAdder Create()
        {
            var not = new Not();
            var nAnd = new NAnd(not);
            var or = new Or(not, nAnd);
            return new BitAdder(new XOr(not, nAnd), or);
        }

        [Test]
        [TestCase(false, false, false, false, false)]
        [TestCase(false, false, true, false, true)]
        [TestCase(false, true, false, false, true)]
        [TestCase(false, true, true, true, false)]
        [TestCase(true, false, false, false, true)]
        [TestCase(true, false, true, true, false)]
        [TestCase(true, true, false, true, false)]
        [TestCase(true, true, true, true, true)]
        public void CanAddNumbers(bool carryIn, bool a, bool b, bool carryOut, bool sum)
        {
            var sut = Create();

            var (result, carry) = sut.Add(a, b, carryIn);

            Assert.AreEqual(sum, result);
            Assert.AreEqual(carryOut, carry);
        }
    }
}