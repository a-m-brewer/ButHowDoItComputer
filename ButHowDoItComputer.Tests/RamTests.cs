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
            _byteFactory = new ByteFactory(new Base10Converter());
            _converter = new Base10Converter();

            _byteConverter = new ByteToBase10Converter(_byteFactory, _converter);

            _and = new And();
            _not = new Not();
            
            _memoryGateFactory = new MemoryGateFactory(new NAnd(_not, _and));
            _byteMemoryGateFactory = new ByteMemoryGateFactory(_memoryGateFactory, _byteFactory); 
            _byteRegisterFactory = new ByteRegisterFactory(_byteMemoryGateFactory, new ByteEnabler(_and, _byteFactory), _byteFactory);
            
            _inputBus = new Bus(new List<IRegister<IByte>>(), _byteFactory);
            _outputBus = new Bus(new List<IRegister<IByte>>(), _byteFactory);
            _ram = new Ram(_outputBus, _byteRegisterFactory, new Decoder(_not, _and), _and);
        }
        
        [Test]
        public void CanSetMemoryAddressRegister()
        {
            var address = _byteConverter.ToByte(255);
            
            _ram.SetMemoryAddress(address);

            for (var i = 0; i < address.Count; i++)
            {
                Assert.AreEqual(address[i],_ram.MemoryAddressRegister.Data[i]); 
            }
        }

        [Test]
        public void CanPutDataIntoARegister()
        {
            _ram.Set = true;
            
            var writeRegister = _byteRegisterFactory.Create();
            _outputBus.Add(writeRegister);
            writeRegister.ApplyOnce(_byteFactory.Create(255), true);

            _ram.SetMemoryAddress(_byteFactory.Create(0));

            _outputBus.Apply();
            
            Assert.IsTrue(_ram.InternalRegisters[0][0].Data.All(a => a));
        }
    }
}