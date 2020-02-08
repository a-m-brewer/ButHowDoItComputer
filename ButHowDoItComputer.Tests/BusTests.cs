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
        private ByteFactory _byteFactory;
        private MemoryGateFactory _memoryGateFactory;
        private And _and;

        [SetUp]
        public void Setup()
        {
            _byteFactory = new ByteFactory(new Base10Converter());
            _and = new And();
            _memoryGateFactory = new MemoryGateFactory(new NAnd(new Not(), _and));
        }
        
        [Test]
        public void CanCopyFromOneRegisterToAnother()
        {
            var registerOne = CreateRegister();
            var registerTwo = CreateRegister();

            registerOne.Set = true;
            registerOne.Apply(CreateByte(true));
            registerOne.Set = false;

            var bus = new Bus(new List<IRegister<IByte>> {registerOne, registerTwo}, _byteFactory)
            {
                [0] = {Enable = true}, [1] = {Set = true}
            };


            bus.Apply();
            
            foreach (var bit in bus[1].Data)
            {
                Assert.AreEqual(true, bit);
            }
        }

        private ByteRegister CreateRegister()
        {
            return new ByteRegister(new ByteMemoryGate(_memoryGateFactory, _byteFactory),
                                   new ByteEnabler(_and, _byteFactory), _byteFactory);
        }

        private static IByte CreateByte(bool state)
        {
            return new Byte(Enumerable.Range(0, 8).Select(s => state).ToArray());
        }
    }
}