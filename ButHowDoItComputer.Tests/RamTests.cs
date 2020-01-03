using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Components;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Gates.Interfaces;
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
        private RegisterFactory _registerFactory;
        private Not _not;
        private Bus _inputBus;
        private Bus _outputBus;
        private Ram _ram;

        [SetUp]
        public void Setup()
        {
            _bitFactory = new BitFactory();
            _byteFactory = new ByteFactory(_bitFactory);
            _converter = new Base10Converter(_bitFactory);

            _byteConverter = new ByteToBase10Converter(_bitFactory, _byteFactory, _converter);

            _and = new And(_bitFactory);
            _not = new Not(_bitFactory);
            
            _memoryGateFactory = new MemoryGateFactory(new NAnd(_not, _and), _bitFactory);
            _byteMemoryGateFactory = new ByteMemoryGateFactory(_memoryGateFactory, _byteFactory); 
            _registerFactory = new RegisterFactory(_byteMemoryGateFactory, new ByteEnabler(_and, _byteFactory), _byteFactory, _bitFactory);
            
            _inputBus = new Bus(new List<IRegister>(), _byteFactory);
            _outputBus = new Bus(new List<IRegister>(), _byteFactory);
            _ram = new Ram(_inputBus, _outputBus, _registerFactory, _bitFactory, new Decoder(_not, _and, _bitFactory), _and);
        }
        
        [Test]
        public void CanSetMemoryAddressRegister()
        {
            var address = _byteConverter.ToByte(255);
            
            _ram.SetMemoryAddress(address);

            for (var i = 0; i < address.Count; i++)
            {
                Assert.AreEqual(address[i].State,_ram.MemoryAddressRegister.Byte[i].State); 
            }
        }

        [Test]
        public void CanPutDataIntoARegister()
        {
            var writeRegister = _registerFactory.Create();
            var readRegister = _registerFactory.Create();
            _outputBus.Add(writeRegister); 
            _outputBus.Add(readRegister); 
            
            var address = _byteConverter.ToByte(0);
            _ram.SetMemoryAddress(address);

            var data = _byteConverter.ToByte(255);

            writeRegister.Set.State = true;
            writeRegister.Enable.State = true;
            writeRegister.Apply(data);
            writeRegister.Set.State = false;
            _outputBus.Apply();
            
            _ram.Set.State = true;
            _ram.Apply();
            _outputBus.Apply();
            _ram.Set.State = false;
            
            writeRegister.Set.State = true;
            writeRegister.Apply(_byteConverter.ToByte(100));
            writeRegister.Set.State = false;

            readRegister.Set.State = true;
            _outputBus.Apply();
            readRegister.Set.State = false;

            writeRegister.Enable.State = false; 
            
            _ram.Enable.State = true;
            readRegister.Set.State = true;
            _ram.Apply();
            _outputBus.Apply();
            readRegister.Set.State = false;
            
            // TODO: write the asserts for this test.
            // TODO: maybe make the api easier
            Assert.Fail();            
        }
    }
}