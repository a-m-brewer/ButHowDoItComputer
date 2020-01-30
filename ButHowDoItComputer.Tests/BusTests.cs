using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class BusTests
    {
        private BitFactory _bitFactory;
        private ByteFactory _byteFactory;
        private MemoryGateFactory _memoryGateFactory;
        private And _and;

        [SetUp]
        public void Setup()
        {
            _bitFactory = new BitFactory();
            _byteFactory = new ByteFactory(_bitFactory, new Base10Converter(_bitFactory));
            _and = new And(_bitFactory);
            _memoryGateFactory = new MemoryGateFactory(new NAnd(new Not(_bitFactory), _and), _bitFactory);
        }
        
        [Test]
        public void CanCopyFromOneRegisterToAnother()
        {
            var registerOne = CreateRegister();
            var registerTwo = CreateRegister();

            registerOne.Set.State = true;
            registerOne.Apply(CreateByte(true));
            registerOne.Set.State = false;
            
            var bus = new Bus(new List<IRegister<IByte>> {registerOne, registerTwo}, _byteFactory);

            bus[0].Enable.State = true;
            bus[1].Set.State = true;

            bus.Apply();
            
            foreach (var bit in bus[1].Data)
            {
                Assert.AreEqual(true, bit.State);
            }
        }

        private ByteRegister CreateRegister()
        {
            return new ByteRegister(new ByteMemoryGate(_memoryGateFactory, _byteFactory),
                                   new ByteEnabler(_and, _byteFactory), _byteFactory, _bitFactory);
        }

        private IByte CreateByte(bool state)
        {
            return new Byte(Enumerable.Range(0, 8).Select(s => _bitFactory.Create(state)).ToArray(), _bitFactory);
        }
    }
}