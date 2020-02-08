using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class BitAdderTests
    {
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

        public BitAdder Create()
        {
            var and = new And();
            var not = new Not();
            var nAnd = new NAnd(not, and);
            var or = new Or(not, nAnd);
            return new BitAdder(new XOr(not, nAnd), or, and);
        }
    }
}