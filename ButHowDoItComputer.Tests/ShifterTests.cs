using ButHowDoItComputer.Components;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class ShifterTests
    {
        [SetUp]
        public void Setup()
        {
            _byteFactory = new ByteFactory(new Base10Converter());
            _and = new And();
            _memoryGateFactory = new MemoryGateFactory(new NAnd(new Not(), _and));
        }

        private ByteFactory _byteFactory;
        private And _and;
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

            var sut = new LeftShifter(_byteFactory, new ByteLeftShifter(_byteFactory));

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

            var sut = new RightShifter(_byteFactory, new ByteRightShifter(_byteFactory));

            sut.Apply(inputRegister, outputRegister);

            for (var i = 0; i < afterByte.Count; i++) Assert.AreEqual(afterByte[i], outputRegister.Output[i]);
        }
    }
}