using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class RegisterTests
    {
        private BitFactory _bitFactory;
        private ByteFactory _byteFactory;
        private MemoryGateFactory _memoryGateFactory;
        private ByteMemoryGate _byteMemoryGate;
        private And _and;
        private ByteEnabler _byteEnabler;
        private Register _sut;

        [SetUp]
        public void Setup()
        {
            _bitFactory = new BitFactory();
            _byteFactory = new ByteFactory(_bitFactory, new Base10Converter(_bitFactory));
            _and = new And(_bitFactory);
            _memoryGateFactory = new MemoryGateFactory(new NAnd(new Not(_bitFactory), _and), _bitFactory); 
            _byteMemoryGate = new ByteMemoryGate(_memoryGateFactory, _byteFactory);
            _byteEnabler = new ByteEnabler(_and, _byteFactory);
            _sut = new Register(_byteMemoryGate, _byteEnabler, _byteFactory, _bitFactory);
        }

        [Test]
        [TestCase(false, false, false)]
        [TestCase(false, false, true)]
        [TestCase(false, true, false)]
        [TestCase(false, true, true)]
        [TestCase(true, false, false)]
        [TestCase(true, false, true)]
        [TestCase(true, true, false)]
        [TestCase(true, true, true)]
        public void TestGateWorksAsIntended(bool input, bool set, bool enable)
        {
            var expected = input && set && enable;
            var bits = Enumerable.Range(0, 8).Select(s => _bitFactory.Create(input)).ToArray();
            _sut.Set = new Bit(set);
            _sut.Enable = new Bit(enable);
            var result = _sut.Apply(new Byte(bits, _bitFactory));
            Assert.AreEqual(expected, result.All(a => a.State));
        }
    }
}