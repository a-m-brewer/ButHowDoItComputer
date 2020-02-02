using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Parts.Factories;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class RamTests
    {
        private BitFactory _bitFactory;
        private Base10Converter _converter;
        private ByteFactory _byteFactory;
        private ByteToBase10Converter _byteConverter;
        private MemoryGateFactory _memoryGateFactory;
        private ByteMemoryGateFactory _byteMemoryGateFactory;
        private And _and;
        private ByteRegisterFactory _byteRegisterFactory;
        private Not _not;
        private Bus _inputBus;
        private Bus _outputBus;
        private Ram _ram;

        [SetUp]
        public void Setup()
        {
            _bitFactory = new BitFactory();
            _byteFactory = new ByteFactory(_bitFactory, new Base10Converter(_bitFactory));
            _converter = new Base10Converter(_bitFactory);

            _byteConverter = new ByteToBase10Converter(_bitFactory, _byteFactory, _converter);

            _and = new And(_bitFactory);
            _not = new Not(_bitFactory);
            
            _memoryGateFactory = new MemoryGateFactory(new NAnd(_not, _and), _bitFactory);
            _byteMemoryGateFactory = new ByteMemoryGateFactory(_memoryGateFactory, _byteFactory); 
            _byteRegisterFactory = new ByteRegisterFactory(_byteMemoryGateFactory, new ByteEnabler(_and, _byteFactory), _byteFactory, _bitFactory);
            
            _inputBus = new Bus(new List<IRegister<IByte>>(), _byteFactory);
            _outputBus = new Bus(new List<IRegister<IByte>>(), _byteFactory);
            _ram = new Ram(_outputBus, _byteRegisterFactory, _bitFactory, new Decoder(_not, _and, _bitFactory), _and);
        }
        
        [Test]
        public void CanSetMemoryAddressRegister()
        {
            var address = _byteConverter.ToByte(255);
            
            _ram.SetMemoryAddress(address);

            for (var i = 0; i < address.Count; i++)
            {
                Assert.AreEqual(address[i].State,_ram.MemoryAddressRegister.Data[i].State); 
            }
        }

        [Test]
        public void CanPutDataIntoARegister()
        {
            var writeRegister = _byteRegisterFactory.Create();
            var readRegister = _byteRegisterFactory.Create();
            _outputBus.Add(writeRegister); 
            _outputBus.Add(readRegister); 
            
            // set address register to first register of ram
            var address = _byteConverter.ToByte(0);
            _ram.SetMemoryAddress(address);

            // byte where all bits are true
            var data = _byteConverter.ToByte(255);
            
            // store that data in the write register and place on bus
            writeRegister.ApplyOnce(data, true);
            _outputBus.Apply();
            
            // bus should be the data byte defined above
            Assert.IsTrue(_outputBus.State.All(a => a.State));
            
            // move whats in the Io bus into the currently selected ram register
            _ram.ApplyState();

            // make a different byte
            var expected = _byteConverter.ToByte(100);
            
            // save it to the write register
            writeRegister.ApplyOnce(expected, true);

            // move the value in write register to read register so that it is different from what the ram is.
            readRegister.Set.State = true;
            _outputBus.Apply();
            readRegister.Set.State = false;

            // make sure that happend
            for (var i = 0; i < readRegister.Data.Count; i++)
            {
                Assert.AreEqual(expected[i].State, readRegister.Data[i].State);
            }

            // disable the write register so that its value does not go on the bus.
            writeRegister.Enable.State = false; 
            
            // move what is in ram to read register
            readRegister.Set.State = true;
            _ram.ApplyEnable();
            readRegister.Set.State = false;
            
            // check the read register is now what was in ram.
            Assert.IsTrue(readRegister.Data.All(a => a.State));
        }
    }
}