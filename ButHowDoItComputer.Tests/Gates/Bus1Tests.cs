using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Gates
{
    [TestFixture]
    public class Bus1Tests
    {
        [SetUp]
        public void Setup()
        {
            _sut = TestUtils.CreateBus1();
            _base10ToByte = TestUtils.CreateByteToBase10Converter();
        }

        private Bus1<IByte> _sut;
        private ByteToBase10Converter _base10ToByte;

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Bus1EnablesAndDisablesInput(bool bus1)
        {
            var expected = bus1 ? _base10ToByte.ToByte(1) : _base10ToByte.ToByte(255);

            var input = _base10ToByte.ToByte(255);

            var result = _sut.Apply(input, bus1);

            for (var i = 0; i < expected.Count; i++) Assert.AreEqual(expected[i], result[i]);
        }
    }
}