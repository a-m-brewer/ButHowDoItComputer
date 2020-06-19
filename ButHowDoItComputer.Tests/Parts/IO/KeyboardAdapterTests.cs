using System;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Codes.ASCII;
using ButHowDoItComputer.DataTypes.Factories;
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
        private KeyboardAdapter<IList<bool>> _sut;
        private SixteenBitFactory _sixteenBitFactory;
        private ByteToAsciiConverter _asciiConverter;
        private KeyboardBuffer<IList<bool>> _keyboardBuffer;

        [SetUp]
        public void Setup()
        {
            _inputDevice = new Mock<IInputDevice>();

            _inputDevice.Setup(s => s.Get()).Returns("a");

            _asciiConverter = new ByteToAsciiConverter(new ByteToBase10Converter(new ByteFactory(new Base10Converter()), new Base10Converter()));
            
            var ui = new UserInput(_inputDevice.Object, _asciiConverter);

            _sixteenBitFactory = new SixteenBitFactory(new Base10Converter());
            
            _keyboardBuffer = new KeyboardBuffer<IList<bool>>(_sixteenBitFactory, ui);

            var busDataTypeRegister = new BusDataTypeRegister<IList<bool>>(
                new BusDataTypeMemoryGate<IList<bool>>(new MemoryGateFactory(new NAnd(new Not())),
                    _sixteenBitFactory, 16), new BusDataTypeEnabler<IList<bool>>(_sixteenBitFactory),
                _sixteenBitFactory, bit => { });

            _sut = new KeyboardAdapter<IList<bool>>(_keyboardBuffer, busDataTypeRegister, _sixteenBitFactory, new Not(),
                new MemoryGate(new NAnd(new Not())),
                new XOr(new Not(), new NAnd(new Not())));
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

            var actual = _asciiConverter.ToAscii(_sut.Ouput.Take(8).ToList());
            
            Assert.AreEqual("a", actual);
        }
    }
}