using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Gates
{
    [TestFixture]
    public class ByteMemoryGateTests
    {
        [Test]
        public void CanKeepTheValueOfAByte()
        {
            var sut = new ByteMemoryGate(new MemoryGateFactory(new NAnd(new Not(), new And())),
                new ByteFactory(new Base10Converter()));

            var input = new Byte(Enumerable.Range(0, 8).Select(s => true).ToArray());

            var result = sut.Apply(input, true);

            for (var i = 0; i < input.Count; i++) Assert.AreEqual(input[i], result[i]);

            input = new Byte(Enumerable.Range(0, 8).Select(s => false).ToArray());

            result = sut.Apply(input, false);

            for (var i = 0; i < input.Count; i++) Assert.AreEqual(true, result[i]);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void CanStoreAByte(bool expected)
        {
            var sut = new ByteMemoryGate(new MemoryGateFactory(new NAnd(new Not(), new And())),
                new ByteFactory(new Base10Converter()));

            var input = new Byte(Enumerable.Range(0, 8).Select(s => expected).ToArray());

            var result = sut.Apply(input, true);

            for (var i = 0; i < input.Count; i++) Assert.AreEqual(input[i], result[i]);
        }
    }
}