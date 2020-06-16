using System.Linq;
using ButHowDoItComputer.Codes.ASCII;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.BusDataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Parts.IO;
using ButHowDoItComputer.Utils;
using ButHowDoItComputer.Utils.Interfaces;
using Moq;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Parts.IO
{
    [TestFixture]
    public class KeyboardAdapterTests
    {
        private Mock<IInputDevice> _inputDevice;
        private KeyboardAdapter<ISixteenBit> _sut;
        private SixteenBitFactory _sixteenBitFactory;
        private ByteToAsciiConverter _asciiConverter;
        private KeyboardBuffer<ISixteenBit> _keyboardBuffer;

        [SetUp]
        public void Setup()
        {
            _inputDevice = new Mock<IInputDevice>();

            _inputDevice.Setup(s => s.Get()).Returns("a");

            _asciiConverter = new ByteToAsciiConverter(new ByteToBase10Converter(new ByteFactory(new Base10Converter()), new Base10Converter()));
            
            var ui = new UserInput(_inputDevice.Object, _asciiConverter);

            _sixteenBitFactory = new SixteenBitFactory(new Base10Converter());
            
            _keyboardBuffer = new KeyboardBuffer<ISixteenBit>(_sixteenBitFactory, ui);

            var busDataTypeRegister = new BusDataTypeRegister<ISixteenBit>(
                new BusDataTypeMemoryGate<ISixteenBit>(new MemoryGateFactory(new NAnd(new Not(), new And())),
                    _sixteenBitFactory, 16), new BusDataTypeEnabler<ISixteenBit>(new And(), _sixteenBitFactory),
                _sixteenBitFactory, bit => { });

            _sut = new KeyboardAdapter<ISixteenBit>(_keyboardBuffer, busDataTypeRegister, _sixteenBitFactory, new Not(),
                new MemoryGate(new NAnd(new Not(), new And())), new And(),
                new XOr(new Not(), new NAnd(new Not(), new And())));
        }

        [Test]
        public void KeycodePutOnBus()
        {
            _keyboardBuffer.Get();
            _sut.Input = _sixteenBitFactory.CreateParams(true, true, true, true, false, false, false, false);

            _sut.Set = true;
            _sut.DataAddress = true;
            _sut.InputOutput = true;
            
            _sut.Apply();
            
            _sut.Set = false;
            _sut.DataAddress = false;
            _sut.InputOutput = false;

            _sut.Enable = true;
            
            _sut.Apply();

            var actual = _asciiConverter.ToAscii(new Byte(_sut.Ouput.Take(8).ToList()));
            
            Assert.AreEqual("a", actual);
        }
    }
}