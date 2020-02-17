using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class ByteEnablerTests
    {
        [SetUp]
        public void Setup()
        {
            _byteFactory = new ByteFactory(new Base10Converter());

            _sut = new ByteEnabler(new And(), _byteFactory);
        }

        private ByteEnabler _sut;
        private ByteFactory _byteFactory;

        [Test]
        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void EnablerFitsTruthTable(bool input, bool enabler)
        {
            var expected = enabler && input;
            var bits = Enumerable.Range(0, 8).Select(s => input).ToArray();
            var result = _sut.Apply(new Byte(bits), enabler);
            Assert.AreEqual(expected, result.All(a => a));
        }
    }
}