using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class ByteComparatorTests
    {
        [SetUp]
        public void Setup()
        {
            _byteFactory = new ByteFactory(new Base10Converter());
            _and = new And();
            _not = new Not();
            _nAnd = new NAnd(_not, _and);
            _or = new Or(_not, _nAnd);
            _xOr = new XOr(_not, _nAnd);
            _bitComparator = new BitComparator(_xOr, _and, _or, _not);
            _byteToBase10 = new ByteToBase10Converter(_byteFactory, new Base10Converter());
            _sut = new ByteComparator(_bitComparator, _byteFactory);
        }

        private And _and;
        private Not _not;
        private NAnd _nAnd;
        private Or _or;
        private XOr _xOr;
        private BitComparator _bitComparator;
        private ByteComparator _sut;
        private ByteFactory _byteFactory;
        private ByteToBase10Converter _byteToBase10;

        [Test]
        [TestCase(0U, 0U, false, true)]
        [TestCase(0U, 1U, false, false)]
        [TestCase(1U, 0U, true, false)]
        public void CanCompareBytes(uint b1, uint b2, bool aLarger, bool equal)
        {
            var a = _byteToBase10.ToByte(b1);
            var b = _byteToBase10.ToByte(b2);

            var (eq, al, o) = _sut.AreEqual(a, b, true, false);

            Assert.AreEqual(aLarger, al);
            Assert.AreEqual(equal, eq);
        }
    }
}