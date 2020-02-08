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

            var actual = _sut.Apply(byteOne, byteTwo, false, opCode);

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual.Output[i]);
            }
        }

        [Test]
        public void CanShiftRight()
        {
            var input = new[] { false, true, false, false, false, false, false, false };
            var expected = new[] { false, false, true, false, false, false, false, false };
            
            var opCode = OpCodes.Shr;

            var actual = _sut.Apply(_byteFactory.Create(input), _byteFactory.Create(0), false, opCode);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual.Output[i]);
            }
        }

        [Test]
        public void CanShiftLeft()
        {
            var input = new[] { false, true, false, false, false, false, false, false };
            var expected = new[] { true, false, false, false, false, false, false, false };

            var opCode = OpCodes.Shl;

            var actual = _sut.Apply(_byteFactory.Create(input), _byteFactory.Create(0), false, opCode);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual.Output[i]);
            }
        }

        [Test]
        public void CanInvertByte()
        {
            var input = new[] { false, false, false, false, false, false, false, false };
            var expected = input.Select(s => (!s)).ToArray();

            var opCode = OpCodes.Not;

            var actual = _sut.Apply(_byteFactory.Create(input), _byteFactory.Create(0), false, opCode);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual.Output[i]);
            }
        }

        [Test]
        public void CanAndByte()
        {
            var a = new[] { true, true, true, true, true, true, true, true };
            var b = a.Select(s => (!s)).ToArray();
            var expected = b;

            var opCode = OpCodes.And;

            var actual = _sut.Apply(_byteFactory.Create(a), _byteFactory.Create(b), false, opCode);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual.Output[i]);
            }
        }

        [Test]
        public void CanOrByte()
        {
            var a = new[] { true, true, true, true, true, true, true, true };
            var b = a.Select(s => (!s)).ToArray();
            var expected = a;

            var opCode = OpCodes.Or;

            var actual = _sut.Apply(_byteFactory.Create(a), _byteFactory.Create(b), false, opCode);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual.Output[i]);
            }
        }

        [Test]
        public void CanXOrByte()
        {
            var a = new[] { true, true, true, true, true, true, true, true };
            var b = a;
            var expected = a.Select(s => (!s)).ToArray();

            var opCode = OpCodes.XOr;

            var actual = _sut.Apply(_byteFactory.Create(a), _byteFactory.Create(b), false, opCode);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual.Output[i]);
            }
        }

        [Test]
        public void CanCompareByteEqual()
        {
            var a = new[] { true, true, true, true, true, true, true, true };
            var b = a;
            var expected = true;

            var opCode = OpCodes.Cmp;

            var actual = _sut.Apply(_byteFactory.Create(a), _byteFactory.Create(b), false, opCode);

            Assert.AreEqual(expected, actual.Equal);
            Assert.AreEqual(!expected, actual.ALarger);
            Assert.AreEqual(!expected, actual.Output.All(a => a));
        }

        [Test]
        public void CanCompareByteUnEqual()
        {
            var a = new[] { true, true, true, true, true, true, true, true };
            var b = a.Select(s => (!s)).ToArray(); ;
            var expected = true;

            var opCode = OpCodes.Cmp;

            var actual = _sut.Apply(_byteFactory.Create(a), _byteFactory.Create(b), false, opCode);

            Assert.AreEqual(!expected, actual.Equal);
            Assert.AreEqual(expected, actual.ALarger);
            Assert.True(actual.Output.Any(a => a));
        }
    }
}
