using ButHowDoItComputer.Components;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Utils;
using NUnit.Framework;
using System.Linq;

namespace ButHowDoItComputer.Tests
{ 
    [TestFixture]
    public class ArithmaticLogicUnitTests
    {
        private ArithmeticLogicUnit _sut;
        private ByteToBase10Converter _base10ToByte;
        private ByteFactory _byteFactory;

        [SetUp]
        public void Setup()
        {
            _sut = TestUtils.CreateArithmeticLogicUnit();
            _base10ToByte = TestUtils.CreateByteToBase10Converter();
            _byteFactory = TestUtils.CreateByteFactory();
        }

        [Test]
        public void CanAddTwoBytes()
        {
            var byteOne = _base10ToByte.ToByte(100);
            var byteTwo = _base10ToByte.ToByte(100);
            var expected = _base10ToByte.ToByte(200);

            var opCode = OpCodes.Add;

            var actual = _sut.Apply(byteOne, byteTwo, new Bit(false), opCode);

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].State, actual.Output[i].State);
            }
        }

        [Test]
        public void CanShiftRight()
        {
            var input = new[] { false.ToBit(), true.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit() };
            var expected = new[] { true.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit() };
            
            var opCode = OpCodes.Shr;

            var actual = _sut.Apply(_byteFactory.Create(input), _byteFactory.Create(0), new Bit(false), opCode);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i].State, actual.Output[i].State);
            }
        }

        [Test]
        public void CanShiftLeft()
        {
            var input = new[] { false.ToBit(), true.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit() };
            var expected = new[] { false.ToBit(), false.ToBit(), true.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit() };

            var opCode = OpCodes.Shl;

            var actual = _sut.Apply(_byteFactory.Create(input), _byteFactory.Create(0), new Bit(false), opCode);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i].State, actual.Output[i].State);
            }
        }

        [Test]
        public void CanInvertByte()
        {
            var input = new[] { false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit() };
            var expected = input.Select(s => (!s.State).ToBit()).ToArray();

            var opCode = OpCodes.Not;

            var actual = _sut.Apply(_byteFactory.Create(input), _byteFactory.Create(0), new Bit(false), opCode);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i].State, actual.Output[i].State);
            }
        }

        [Test]
        public void CanAndByte()
        {
            var a = new[] { true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit() };
            var b = a.Select(s => (!s.State).ToBit()).ToArray();
            var expected = b;

            var opCode = OpCodes.And;

            var actual = _sut.Apply(_byteFactory.Create(a), _byteFactory.Create(b), new Bit(false), opCode);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i].State, actual.Output[i].State);
            }
        }

        [Test]
        public void CanOrByte()
        {
            var a = new[] { true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit() };
            var b = a.Select(s => (!s.State).ToBit()).ToArray();
            var expected = a;

            var opCode = OpCodes.Or;

            var actual = _sut.Apply(_byteFactory.Create(a), _byteFactory.Create(b), new Bit(false), opCode);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i].State, actual.Output[i].State);
            }
        }

        [Test]
        public void CanXOrByte()
        {
            var a = new[] { true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit() };
            var b = a;
            var expected = a.Select(s => (!s.State).ToBit()).ToArray();

            var opCode = OpCodes.XOr;

            var actual = _sut.Apply(_byteFactory.Create(a), _byteFactory.Create(b), new Bit(false), opCode);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i].State, actual.Output[i].State);
            }
        }

        [Test]
        public void CanCompareByteEqual()
        {
            var a = new[] { true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit() };
            var b = a;
            var expected = true;

            var opCode = OpCodes.Cmp;

            var actual = _sut.Apply(_byteFactory.Create(a), _byteFactory.Create(b), new Bit(false), opCode);

            Assert.AreEqual(expected, actual.Equal.State);
            Assert.AreEqual(!expected, actual.ALarger.State);
            Assert.AreEqual(!expected, actual.Output.All(a => a.State));
        }

        [Test]
        public void CanCompareByteUnEqual()
        {
            var a = new[] { true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit(), true.ToBit() };
            var b = a.Select(s => (!s.State).ToBit()).ToArray(); ;
            var expected = true;

            var opCode = OpCodes.Cmp;

            var actual = _sut.Apply(_byteFactory.Create(a), _byteFactory.Create(b), new Bit(false), opCode);

            Assert.AreEqual(!expected, actual.Equal.State);
            Assert.AreEqual(expected, actual.ALarger.State);
            Assert.True(actual.Output.Any(a => a.State));
        }
    }
}
