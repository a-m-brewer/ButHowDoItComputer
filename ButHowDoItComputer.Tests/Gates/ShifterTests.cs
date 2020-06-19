using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.BusDataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
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

            var beforeByte = new Byte(new[]
            {
                false,
                true,
                false,
                false,

                false,
                false,
                true,
                false
            });

            var afterByte = new Byte(new[]
            {
                false,
                false,
                true,
                false,

                false,
                false,
                false,
                true
            });

            inputRegister.Input = beforeByte;

            var sut = new LeftShifter<IByte>(_byteFactory, new BusDataTypeLeftShifter<IByte>(_byteFactory));

            sut.Apply(inputRegister, outputRegister);

            for (var i = 0; i < afterByte.Count; i++) Assert.AreEqual(afterByte[i], outputRegister.Output[i]);
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

            var beforeByte = new Byte(new[]
            {
                false,
                true,
                false,
                false,

                false,
                false,
                true,
                false
            });

            var afterByte = new Byte(new[]
            {
                true,
                false,
                false,
                false,

                false,
                true,
                false,
                false
            });

            inputRegister.Input = beforeByte;

            var sut = new RightShifter<IByte>(_byteFactory, new BusDataTypeRightShifter<IByte>(_byteFactory));

            sut.Apply(inputRegister, outputRegister);

            for (var i = 0; i < afterByte.Count; i++) Assert.AreEqual(afterByte[i], outputRegister.Output[i]);
        }
    }
}