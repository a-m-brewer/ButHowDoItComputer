using System.Linq;
using ButHowDoItComputer.Components.Factories;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class ComputerStateTests
    {
        private ComputerState _sut;
        private PinStates _pinStates;
        private ByteFactory _byteFactory;
        private IByte _fullByte;

        [SetUp]
        public void Setup()
        {
            _byteFactory = TestUtils.CreateByteFactory();
            _fullByte = _byteFactory.Create(255);
            var bus = new Bus<IByte>(new BusMessage<IByte> {Data = new Byte(), Name = "Bus"});
            var byteRegisterFactory = TestUtils.CreateByteRegisterFactory();
            var ram = TestUtils.CreateRam(bus);
            _pinStates = new PinStates();
            _sut = new ComputerState(byteRegisterFactory, ram, TestUtils.CreateBus1Factory(),
                new ArithmeticLogicUnitFactory(), TestUtils.CreateCaezRegisterFactory(), bus);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void PinStatesMapsToGeneralPurposeRegisters(int index)
        {
            _pinStates.GeneralPurposeRegisters[index].Set = true;
            _pinStates.GeneralPurposeRegisters[index].Enable = true;

            _sut.UpdatePins(_pinStates);
            
            Assert.IsTrue(_sut.GeneralPurposeRegisters[index].Set);
            Assert.IsTrue(_sut.GeneralPurposeRegisters[index].Enable);
        }

        [Test]
        public void PinStatesMapsToIr()
        {
            _pinStates.Ir.Set = true;
            
            _sut.UpdatePins(_pinStates);
            
            Assert.IsTrue(_sut.Ir.Set);
            Assert.IsTrue(_sut.Ir.Enable);
        }

        [Test]
        public void PinStatesForIarMap()
        {
            _pinStates.Iar.Set = true;
            _pinStates.Iar.Enable = true;
            
            _sut.UpdatePins(_pinStates);
            
            Assert.IsTrue(_sut.Iar.Set);
            Assert.IsTrue(_sut.Iar.Enable);
        }

        [Test]
        public void PinStatesForAccMap()
        {
            _pinStates.Acc.Set = true;
            _pinStates.Acc.Enable = true;
            
            _sut.UpdatePins(_pinStates);
            
            Assert.IsTrue(_sut.Acc.Set);
            Assert.IsTrue(_sut.Acc.Enable);
        }
        
        [Test]
        public void PinStatesForRamMap()
        {
            _pinStates.Ram.Set = true;
            _pinStates.Ram.Enable = true;
            
            _sut.UpdatePins(_pinStates);
            
            Assert.IsTrue(_sut.Ram.Set);
            Assert.IsTrue(_sut.Ram.Enable);
        }

        [Test]
        public void PinStatesForMarMap()
        {
            _pinStates.Mar = true;
            
            _sut.UpdatePins(_pinStates);
            
            Assert.IsTrue(_sut.Ram.MemoryAddressRegister.Set);
            Assert.IsTrue(_sut.Ram.MemoryAddressRegister.Enable);
        }

        [Test]
        public void PinStatesMapForTmp()
        {
            _pinStates.Tmp.Set = true;
            
            _sut.UpdatePins(_pinStates);
            
            Assert.IsTrue(_sut.Tmp.Set);
            Assert.IsTrue(_sut.Tmp.Enable);
        }

        [Test]
        public void PinStatesMapsToBus1()
        {
            _pinStates.Bus1 = true;
            
            _sut.UpdatePins(_pinStates);
            
            Assert.IsTrue(_sut.Bus1.Set);
        }

        [Test]
        public void PinStatesMapsOpCode()
        {
            _pinStates.Op = new Op { One = true, Two = true, Three = true};
            
            _sut.UpdatePins(_pinStates);
            
            Assert.IsTrue(_sut.Alu.Op.One);
            Assert.IsTrue(_sut.Alu.Op.Two);
            Assert.IsTrue(_sut.Alu.Op.Three);
        }

        [Test]
        public void FlagsAreMappedToCaezRegister()
        {
            _pinStates.Flags = true;
            
            _sut.UpdatePins(_pinStates);
            
            Assert.IsTrue(_sut.Flags.Set);
            Assert.IsTrue(_sut.Flags.Enable);
        }

        [Test]
        public void AluOutputMapsToFlagsRegisterInput()
        {
            var byteOne = _byteFactory.Create(100);
            var byteTwo = _byteFactory.Create(100);

            var opCode = OpCodes.Add;

            _sut.Flags.Set = true;

            var result = _sut.Alu.Apply(byteOne, byteTwo, false, opCode);

            Assert.AreEqual(result.CarryOut, _sut.Flags.Data.C);
            Assert.AreEqual(result.ALarger, _sut.Flags.Data.A);
            Assert.AreEqual(result.Equal, _sut.Flags.Data.E);
            Assert.AreEqual(result.Zero, _sut.Flags.Data.Z);
        }

        [Test]
        public void UpdatingAluUpdatesAcc()
        {
            var byteOne = _byteFactory.Create(100);
            var byteTwo = _byteFactory.Create(100);
            var expected = _byteFactory.Create(200);

            var opCode = OpCodes.Add;

            _sut.Acc.Set = true;

            _sut.Alu.Apply(byteOne, byteTwo, false, opCode);

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], _sut.Acc.Data[i]);
            }
        }

        [Test]
        public void TmpUpdatesBus1()
        {
            var byteTest = _byteFactory.Create(255);
            
            _sut.Tmp.ApplyOnce(byteTest, true);
            
            Assert.IsTrue(_sut.Bus1.Input.All(a => a));
            Assert.IsTrue(_sut.Bus1.Output.All(a => a));
        }

        [Test]
        public void AccUpdatesBus()
        {
            _sut.Acc.ApplyOnce(_fullByte, true);
            
            Assert.IsTrue(_sut.Bus.Data.Data.All(a => a));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GeneralPurposeRegistersCanUpdateBus(int register)
        {
            _sut.GeneralPurposeRegisters[register].ApplyOnce(_fullByte, true);
            
            Assert.IsTrue(_sut.Bus.Data.Data.All(a => a));
        }
        
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void BusCanUpdateGeneralPurposeRegisters(int register)
        {
            _sut.GeneralPurposeRegisters[register].Set = true;
            _sut.Bus.UpdateData(new BusMessage<IByte> {Data = _fullByte, Name = "fromBus"});
            _sut.Bus.UpdateSubs();
            Assert.IsTrue(_sut.GeneralPurposeRegisters[register].Data.All(a => a));
        }

        [Test]
        public void UpdatingBusUpdatesChildren()
        {
            _sut.GeneralPurposeRegisters[1].Set = true;
            _sut.GeneralPurposeRegisters[0].ApplyOnce(_fullByte, true);
            Assert.IsTrue(_sut.GeneralPurposeRegisters[1].Data.All(a => a));
        }

        [Test]
        public void UpdatingBusUpdatesAlu()
        {
            _sut.Alu.Op = OpCodes.Add;
            _sut.Acc.Set = true;
            _sut.Acc.Enable = false;
            _sut.Bus.UpdateData(new BusMessage<IByte> {Data = _fullByte, Name = "fromBus"});
            _sut.Bus.UpdateSubs();
            
            Assert.IsTrue(_sut.Alu.InputA.All(a => a));
            Assert.IsTrue(_sut.Acc.Data.All(a => a));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void AllGeneralPurposeRegistersAreNamed(int register)
        {
            var name = _sut.GeneralPurposeRegisters[register].Name;
            Assert.AreEqual($"GeneralPurposeRegister_{register}", name);
        }

        [Test]
        public void CanUpdateMarFromBus()
        {
            _sut.Ram.MemoryAddressRegister.Set = true;
            _sut.Bus.UpdateData(new BusMessage<IByte> {Data = _fullByte, Name = "fromBus"});
            _sut.Bus.UpdateSubs();
            
            Assert.IsTrue(_sut.Ram.MemoryAddressRegister.Data.All(a => a));
        }

        [Test]
        public void IarCanUpdateBus()
        {
            _sut.Iar.ApplyOnce(_fullByte, true);
            Assert.IsTrue(_sut.Bus.Data.Data.All(a => a));
        }

        [Test]
        public void BusCanUpdateIar()
        {
            _sut.Iar.Set = true;
            _sut.Bus.UpdateData(new BusMessage<IByte> {Data = _fullByte, Name = "fromBus"});
            _sut.Bus.UpdateSubs();
            Assert.IsTrue(_sut.Iar.Data.All(a => a));
        }

        [Test]
        public void BusCanUpdateIr()
        {
            _sut.Ir.Set = true;
            _sut.Bus.UpdateData(new BusMessage<IByte> {Data = _fullByte, Name = "fromBus"});
            _sut.Bus.UpdateSubs();
            Assert.IsTrue(_sut.Ir.Data.All(a => a));
        }

        [Test]
        public void Bus1UpdatesAluInputB()
        {
            _sut.Alu.Op = OpCodes.Add;
            _sut.Acc.Set = true;
            _sut.Acc.Enable = false;

            _sut.Bus1.Input = _fullByte;
            _sut.Bus1.Apply();
            
            Assert.IsTrue(_sut.Alu.InputB.All(a => a));
            Assert.IsTrue(_sut.Acc.Data.All(a => a));
        }

        [Test]
        public void RamIoUpdatesBus()
        {
            _sut.Ram.InternalRegisters[0][0].ApplyOnce(_fullByte);
            _sut.Ram.SetMemoryAddress(_byteFactory.Create(0));
            _sut.Ram.Enable = true;
            
            _sut.Ram.Apply();

            Assert.IsTrue(_sut.Bus.Data.Data.All(a => a));
        }

        [Test]
        public void FlagsRegisterUpdatesCarryIn()
        {
            _sut.Flags.Input = new Caez {C = true};
            _sut.Flags.Set = true;
            _sut.Flags.Apply();
            Assert.IsTrue(_sut.Alu.CarryIn);
        }
    }
}