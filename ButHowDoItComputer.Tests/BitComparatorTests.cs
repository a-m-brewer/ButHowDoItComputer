using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class BitComparatorTests
    {
        private BitComparator _bitComparator;
        private And _and;
        private Not _not;
        private NAnd _nAnd;
        private Or _or;
        private XOr _xOr;

        [SetUp]
        public void Setup()
        {
            _and = new And();
            _not = new Not();
            _nAnd = new NAnd(_not, _and);
            _or = new Or(_not, _nAnd);
            _xOr = new XOr(_not, _nAnd);
            _bitComparator = new BitComparator(_xOr, _and, _or, _not);
        }

        [Test]
        [TestCase(false, false, true, false, false)]
        [TestCase(false, true, false, false, true)]
        [TestCase(true, false, false, true, true)]
        [TestCase(true, true, true, false, false)]
        public void CanCompareTwoBits(bool a, bool b, bool equal, bool aLarger, bool output)
        {
            var (eq, lg, op) = _bitComparator.AreEqual(a, b, true, false);
            
            Assert.AreEqual(equal, eq);
            Assert.AreEqual(aLarger, lg);
            Assert.AreEqual(output, op);
        }

        [Test]
        public void WillReportALargerIfItIsPassedIn()
        {
            var (eq, lg, op) = _bitComparator.AreEqual(false, false, false, true);
            
            Assert.AreEqual(false, eq);
            Assert.AreEqual(true, lg);
            Assert.AreEqual(false, op);
        }
    }
}