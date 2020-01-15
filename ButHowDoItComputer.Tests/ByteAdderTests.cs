using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class ByteAdderTests
    {
        private BitFactory _bitFactory;
        private ByteFactory _byteFactory;
        private ByteAdder _sut;
        private Base10Converter _b10Converter;

        [SetUp]
        public void Setup()
        {
            _bitFactory = new BitFactory();
            _b10Converter = new Base10Converter(_bitFactory);
            _byteFactory = new ByteFactory(_bitFactory, _b10Converter);
            _sut = new ByteAdder(Create(), _byteFactory);
        }
        
        [Test]
        public void CanAddTwoNumbers()
        {
            var a = _byteFactory.Create(100);
            var b = _byteFactory.Create(50);
            var expected = _byteFactory.Create(150);

            var (sum, carry) = _sut.Add(a, b, new Bit(false));

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].State, sum[i].State);
            }
            
            Assert.IsFalse(carry.State);
        }

        [Test]
        public void IfOutputNumberLargerThan255OneIsCarried()
        {
            var a = _byteFactory.Create(255);
            var b = _byteFactory.Create(255);

            var (sum, carry) = _sut.Add(a, b, new Bit(true));
            
            Assert.IsTrue(carry.State);
            Assert.IsTrue(sum.All(s => s.State));
        }
        
        private static BitAdder Create()
        {
            var bitFactory = new BitFactory();
            var and = new And(bitFactory);
            var not = new Not(bitFactory);
            var nAnd = new NAnd(not, and);
            var or = new Or(not, nAnd);
            return new BitAdder(new XOr(not, nAnd), or, and);
        }
    }
}