using ButHowDoItComputer.Components;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class RegisterAdderTests
    {
        private BitFactory _bitFactory;
        private Base10Converter _b10Converter;
        private ByteFactory _byteFactory;
        private IByteAdder _byteAdder;
        private RegisterAdder _sut;

        [SetUp]
        public void Setup()
        {
            _byteAdder = CreateByteAdder();
            _sut = new RegisterAdder(_byteAdder);
        }
        
        [Test]
        public void CanAddTwoNumbers()
        {
            var a = _byteFactory.Create(100);
            var b = _byteFactory.Create(50);
            var expected = _byteFactory.Create(150);

            var registerA = TestUtils.CreateRegister();
            var registerB = TestUtils.CreateRegister();
            var outputRegister = TestUtils.CreateRegister();

            registerA.Input = a;
            registerB.Input = b;

            var carry = _sut.Apply(registerA, registerB, new Bit(false), outputRegister);

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].State, outputRegister.Output[i].State);
            }
            
            Assert.IsFalse(carry.State);
        }

        private IByteAdder CreateByteAdder()
        {
            _bitFactory = new BitFactory();
            _b10Converter = new Base10Converter(_bitFactory);
            _byteFactory = new ByteFactory(_bitFactory, _b10Converter);
            return new ByteAdder(CreateBitAdder(), _byteFactory);
        }
        
        private static BitAdder CreateBitAdder()
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