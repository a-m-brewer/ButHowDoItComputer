using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class ByteComparatorTests
    {
        private BitFactory _bitFactory;
        private And _and;
        private Not _not;
        private NAnd _nAnd;
        private Or _or;
        private XOr _xOr;
        private BitComparator _bitComparator;
        private ByteComparator _sut;
        private ByteFactory _byteFactory;
        private ByteToBase10Converter _byteToBase10;

        [SetUp]
        public void Setup()
        {
            _bitFactory = new BitFactory();
            _byteFactory = new ByteFactory(_bitFactory, new Base10Converter(_bitFactory));
            _and = new And(_bitFactory);
            _not = new Not(_bitFactory);
            _nAnd = new NAnd(_not, _and);
            _or = new Or(_not, _nAnd);
            _xOr = new XOr(_not, _nAnd);
            _bitComparator = new BitComparator(_xOr, _and, _or, _not);
            _byteToBase10 = new ByteToBase10Converter(_bitFactory, _byteFactory, new Base10Converter(_bitFactory));
            _sut = new ByteComparator(_bitComparator, _byteFactory);
        }
        
        [Test]
        [TestCase(0U, 0U, false, true)]
        [TestCase(0U, 1U, false, false)]
        [TestCase(1U, 0U, true, false)]
        public void CanCompareBytes(uint b1, uint b2, bool aLarger, bool equal)
        {
            var a = _byteToBase10.ToByte(b1);
            var b = _byteToBase10.ToByte(b2);

            var (eq, al, o) = _sut.AreEqual(a, b, new Bit(true), new Bit(false));
            
            Assert.AreEqual(aLarger, al.State);
            Assert.AreEqual(equal, eq.State);
        }
    }
}