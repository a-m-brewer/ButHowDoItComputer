using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class ByteEnablerTests
    {
        private ByteEnabler _sut;
        private BitFactory _bitFactory;
        private ByteFactory _byteFactory;

        [SetUp]
        public void Setup()
        {
            _bitFactory = new BitFactory();
            _byteFactory = new ByteFactory(_bitFactory, new Base10Converter(_bitFactory));
            
            _sut = new ByteEnabler(new And(_bitFactory), _byteFactory);
        }

        [Test]
        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void EnablerFitsTruthTable(bool input, bool enabler)
        {
            var expected = enabler && input;
            var bits = Enumerable.Range(0, 8).Select(s => _bitFactory.Create(input)).ToArray();
            var result = _sut.Apply(new Byte(bits, _bitFactory), _bitFactory.Create(enabler));
            Assert.AreEqual(expected, result.All(a => a.State));
        }
    }
}