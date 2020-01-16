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
        private BitFactory _bitFactory;
        private And _and;
        private Not _not;
        private NAnd _nAnd;
        private Or _or;
        private XOr _xOr;

        [SetUp]
        public void Setup()
        {
            _bitFactory = new BitFactory();
            _and = new And(_bitFactory);
            _not = new Not(_bitFactory);
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
            var (eq, lg, op) = _bitComparator.AreEqual(new Bit(a), new Bit(b), new Bit(true), new Bit(false));
            
            Assert.AreEqual(equal, eq.State);
            Assert.AreEqual(aLarger, lg.State);
            Assert.AreEqual(output, op.State);
        }

        [Test]
        public void WillReportALargerIfItIsPassedIn()
        {
            var (eq, lg, op) = _bitComparator.AreEqual(new Bit(false), new Bit(false), new Bit(false), new Bit(true));
            
            Assert.AreEqual(false, eq.State);
            Assert.AreEqual(true, lg.State);
            Assert.AreEqual(false, op.State);
        }
    }
}