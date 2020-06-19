using System;
using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Gates
{
    [TestFixture]
    public class ShifterTests
    {
        [SetUp]
        public void Setup()
        {
            _byteFactory = new ByteFactory(new Base10Converter());
            _memoryGateFactory = new MemoryGateFactory(new NAnd(new Not()));
        }

        private ByteFactory _byteFactory;
        private MemoryGateFactory _memoryGateFactory;

        [Test]
        public void TestLeftShifter()
        {
            var inputRegister = TestUtils.CreateRegister();
            inputRegister.Set = true;
            inputRegister.Enable = true;
            var outputRegister = TestUtils.CreateRegister();
            outputRegister.Set = true;
            outputRegister.Enable = true;

            var beforeByte = new[]
            {
                false,
                true,
                false,
                false,

                false,
                false,
                true,
                false
            };

            var afterByte = new[]
            {
                false,
                false,
                true,
                false,

                false,
                false,
                false,
                true
            };

            inputRegister.Input = beforeByte;

            var sut = new LeftShifter<IList<bool>>(_byteFactory, new BusDataTypeLeftShifter<IList<bool>>(_byteFactory));

            sut.Apply(inputRegister, outputRegister);

            for (var i = 0; i < afterByte.Length; i++) Assert.AreEqual(afterByte[i], outputRegister.Output[i]);
        }

        [Test]
        public void TestRightShifter()
        {
            var inputRegister = TestUtils.CreateRegister();
            inputRegister.Set = true;
            inputRegister.Enable = true;
            var outputRegister = TestUtils.CreateRegister();
            outputRegister.Set = true;
            outputRegister.Enable = true;

            var beforeByte = new[]
            {
                false,
                true,
                false,
                false,

                false,
                false,
                true,
                false
            };

            var afterByte = new[]
            {
                true,
                false,
                false,
                false,

                false,
                true,
                false,
                false
            };

            inputRegister.Input = beforeByte;

            var sut = new RightShifter<IList<bool>>(_byteFactory, new BusDataTypeRightShifter<IList<bool>>(_byteFactory));

            sut.Apply(inputRegister, outputRegister);

            for (var i = 0; i < afterByte.Length; i++) Assert.AreEqual(afterByte[i], outputRegister.Output[i]);
        }
    }
}