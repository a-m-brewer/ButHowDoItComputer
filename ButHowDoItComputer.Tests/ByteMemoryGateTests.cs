using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Factories;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class ByteMemoryGateTests
    {
                [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void CanStoreAByte(bool expected)
        {
            var sut = new ByteMemoryGate(new MemoryGateFactory(new NAnd(new Not(new BitFactory()), new And(new BitFactory())), new BitFactory()), new ByteFactory(new BitFactory()));

            var input = new Byte(Enumerable.Range(0, 8).Select(s => new Bit(expected)).ToArray(), new BitFactory());
            var set = new Bit(true);

            var result = sut.Apply(input, set);

            for (var i = 0; i < input.Count; i++)
            {
                Assert.AreEqual(input[i].State, result[i].State);
            }
        }

        [Test]
        public void CanKeepTheValueOfAByte()
        {
            var sut = new ByteMemoryGate(new MemoryGateFactory(new NAnd(new Not(new BitFactory()), new And(new BitFactory())), new BitFactory()), new ByteFactory(new BitFactory()));

            var input = new Byte(Enumerable.Range(0, 8).Select(s => new Bit(true)).ToArray(), new BitFactory());
            var set = new Bit(true);

            var result = sut.Apply(input, set);

            for (var i = 0; i < input.Count; i++)
            {
                Assert.AreEqual(input[i].State, result[i].State);
            }
            
            input = new Byte(Enumerable.Range(0, 8).Select(s => new Bit(false)).ToArray(), new BitFactory());
            set = new Bit(false);

            result = sut.Apply(input, set);
            
            for (var i = 0; i < input.Count; i++)
            {
                Assert.AreEqual(true, result[i].State);
            }
        }
    }
}