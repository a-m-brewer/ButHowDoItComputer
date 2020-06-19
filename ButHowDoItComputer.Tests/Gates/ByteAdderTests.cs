using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Gates
{
    [TestFixture]
    public class ByteAdderTests
    {
        [SetUp]
        public void Setup()
        {
            _b10Converter = new Base10Converter();
            _byteFactory = new ByteFactory(_b10Converter);
            _sut = new BusDataTypeAdder<IList<bool>>(Create(), _byteFactory);
        }

        private ByteFactory _byteFactory;
        private BusDataTypeAdder<IList<bool>> _sut;
        private Base10Converter _b10Converter;

        private static BitAdder Create()
        {
            var not = new Not();
            var nAnd = new NAnd(not);
            var or = new Or(not, nAnd);
            return new BitAdder(new XOr(not, nAnd), or);
        }

        [Test]
        public void CanAddTwoNumbers()
        {
            var a = _byteFactory.Create(100);
            var b = _byteFactory.Create(50);
            var expected = _byteFactory.Create(150);

            var (sum, carry) = _sut.Add(a, b, false);

            for (var i = 0; i < expected.Count; i++) Assert.AreEqual(expected[i], sum[i]);

            Assert.IsFalse(carry);
        }

        [Test]
        public void IfOutputNumberLargerThan255OneIsCarried()
        {
            var a = _byteFactory.Create(255);
            var b = _byteFactory.Create(255);

            var (sum, carry) = _sut.Add(a, b, true);

            Assert.IsTrue(carry);
            Assert.IsTrue(sum.All(s => s));
        }
    }
}