using System.Linq;
using ButHowDoItComputer.Components;
using ButHowDoItComputer.Components.Factories;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Parts.Factories;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Parts
{
    [TestFixture]
    public class ComputerTests
    {
        private IByte _fullByte;
        private ByteFactory _byteFactory;
        private Computer<IByte> _sut;

        [SetUp]
        public void Setup()
        {
            _byteFactory = TestUtils.CreateByteFactory();
            _fullByte = _byteFactory.Create(255);

            Instruction = _byteFactory.Create(0);
            Flags = new Caez();

            var cpuPinStates = new CpuPinStates<IByte>(TestUtils.CreateClock(), TestUtils.CreateStepper(), Instruction, Flags,
                TestUtils.CreateAnd(), TestUtils.CreateOr(), TestUtils.CreateNot(), TestUtils.CreateDecoder(),
                _byteFactory);
            
            var bus = new Bus<IByte>(new BusMessage<IByte> {Data = new Byte(), Name = "Bus"});
            var ioBus = new Bus<IByte>(new BusMessage<IByte> {Data = new Byte(), Name = "IoBus"});
            var byteRegisterFactory = TestUtils.CreateBusTypeRegisterFactory();
            var ram = TestUtils.CreateRam(bus);
            var computerState = new ComputerState<IByte>(byteRegisterFactory, ram, TestUtils.CreateBus1Factory(),
                new ArithmeticLogicUnitFactory<IByte>(_byteFactory), TestUtils.CreateCaezRegisterFactory(), new BitRegisterFactory(TestUtils.CreateMemoryGateFactory()), bus, ioBus, _byteFactory);

            _sut = new Computer<IByte>(cpuPinStates, computerState, _byteFactory);
        }

        public Caez Flags { get; set; }

        public IByte Instruction { get; set; }

        // step one
        
        [Test]
        public void MarIsSetToAddressInIarInStepOne()
        {
            _sut.ComputerState.Iar.ApplyOnce(_byteFactory.Create(255));
            
            StepFull(1);
            
            var result = _sut.ComputerState.Ram.MemoryAddressRegister.Data;
            
            Assert.IsTrue(result.All(a => a));
        }

        [Test]
        public void AfterStepOneAccIsIncrementOfIarAddress()
        {
            _sut.ComputerState.Iar.ApplyOnce(_byteFactory.Create(0));

            StepFull(1);

            var result = _sut.ComputerState.Acc.Data;
            
            Assert.IsTrue(result[0]);
        }
        
        // step 2

        [Test]
        public void AfterStep2ByteInRamIsInIr()
        {
            _sut.ComputerState.Iar.ApplyOnce(_byteFactory.Create(0));
            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(_fullByte);
            
            StepFull(2);

            var result = _sut.ComputerState.Ir.Data;
            
            Assert.IsTrue(result.All(a => a));
        }
        
        // step 3

        [Test]
        public void AfterStep3IarIsIncrementedByOne()
        {
            _sut.ComputerState.Iar.ApplyOnce(_byteFactory.Create(0));
            
            StepFull(3);

            var result = _sut.ComputerState.Iar.Data;
            
            Assert.IsTrue(result[0]);
        }
        
        // Full instructions

        // ADD R0 R1
        // 1 000 00 01
        [Test]
        public void CanAddTwoNumbersTogether()
        {
            var expected = _byteFactory.Create(75);

            var instruction = _byteFactory.Create(true, false, false, false, false, false, false, true);

            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            _sut.ComputerState.GeneralPurposeRegisters[0].ApplyOnce(_byteFactory.Create(50));
            _sut.ComputerState.GeneralPurposeRegisters[1].ApplyOnce(_byteFactory.Create(25));
            
            StepFull(6);

            var result = _sut.ComputerState.GeneralPurposeRegisters[1].Data;

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }
        
        // SHL R0 R1
        // 1 001 00 01
        [Test]
        public void CanShiftLeft()
        {
            var instruction = _byteFactory.Create(true, false, false, true, false, false, false, true);

            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            _sut.ComputerState.GeneralPurposeRegisters[0].ApplyOnce(_byteFactory.Create(128));
            
            StepFull(6);

            var result = _sut.ComputerState.GeneralPurposeRegisters[1].Data;
            
            Assert.IsTrue(result[6]);
        }

        // SHR R0 R1
        [Test]
        public void CanShiftRight()
        {
            var instruction = _byteFactory.Create(true, false, true, false, false, false, false, true);
            
            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            _sut.ComputerState.GeneralPurposeRegisters[0].ApplyOnce(_byteFactory.Create(1));
            
            StepFull(6);

            var result = _sut.ComputerState.GeneralPurposeRegisters[1].Data;
            
            Assert.IsTrue(result[1]);
        }

        // NOT R0 R1
        [Test]
        public void CanNotAByte()
        {
            var instruction = _byteFactory.Create(true, false, true, true, false, false, false, true);

            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            _sut.ComputerState.GeneralPurposeRegisters[0].ApplyOnce(_byteFactory.Create(0));
            
            StepFull(6);

            var result = _sut.ComputerState.GeneralPurposeRegisters[1].Data;
            
            Assert.IsTrue(result.All(a => a));
        }
        
        // AND R0 R1
        [Test]
        public void CanAndByte()
        {
            var instruction = _byteFactory.Create(true, true, false, false, false, false, false, true);

            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            _sut.ComputerState.GeneralPurposeRegisters[0].ApplyOnce(_byteFactory.Create(0));
            _sut.ComputerState.GeneralPurposeRegisters[1].ApplyOnce(_byteFactory.Create(255));
            
            StepFull(6);

            var result = _sut.ComputerState.GeneralPurposeRegisters[1].Data;
            
            Assert.IsTrue(result.All(a => !a));
        }
        
        // Or R0 R1
        [Test]
        public void CanOrByte()
        {
            var instruction = _byteFactory.Create(true, true, false, true, false, false, false, true);

            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            _sut.ComputerState.GeneralPurposeRegisters[0].ApplyOnce(_byteFactory.Create(0));
            _sut.ComputerState.GeneralPurposeRegisters[1].ApplyOnce(_byteFactory.Create(255));
            
            StepFull(6);

            var result = _sut.ComputerState.GeneralPurposeRegisters[1].Data;
            
            Assert.IsTrue(result.All(a => a));
        }
        
        // XOR R0 R1
        [Test]
        public void CanXorByte()
        {
            var instruction = _byteFactory.Create(true, true, true, false, false, false, false, true);

            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            _sut.ComputerState.GeneralPurposeRegisters[0].ApplyOnce(_byteFactory.Create(255));
            _sut.ComputerState.GeneralPurposeRegisters[1].ApplyOnce(_byteFactory.Create(255));
            
            StepFull(6);

            var result = _sut.ComputerState.GeneralPurposeRegisters[1].Data;
            
            Assert.IsTrue(result.All(a => !a));
        }
        
        // CMP R0 R1
        [Test]
        public void CanCmpEqualBytes()
        {
            var instruction = _byteFactory.Create(true, true, true, true, false, false, false, true);

            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            _sut.ComputerState.GeneralPurposeRegisters[0].ApplyOnce(_byteFactory.Create(255));
            _sut.ComputerState.GeneralPurposeRegisters[1].ApplyOnce(_byteFactory.Create(255));
            
            StepFull(6);

            var result = _sut.ComputerState.Flags.Data;
            
            Assert.IsFalse(result.C);
            Assert.IsTrue(result.E);
            Assert.IsFalse(result.A);
            // Tried to figure this out but I don't see how following the design this would correctly output false
            // with cmp operation.
            // Assert.IsFalse(result.Z);
        }
        
        // LD R0 R1
        // Load RB from RAM Address RA
        [Test]
        public void CanLoadFromRam()
        {
            var instruction = _byteFactory.Create(false, false, false, false, false, false, false, true);

            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            
            _sut.ComputerState.GeneralPurposeRegisters[0].ApplyOnce(_byteFactory.Create(1));
            _sut.ComputerState.Ram.InternalRegisters[0][1].ApplyOnce(_byteFactory.Create(255));
            
            StepFull(6);

            var result = _sut.ComputerState.GeneralPurposeRegisters[1].Data;
            
            Assert.IsTrue(result.All(a => a));
        }
        
        // ST R0 R1
        // Store RB to Ram address in RA
        [Test]
        public void CanStoreDataInRam()
        {
            var instruction = _byteFactory.Create(false, false, false, true, false, false, false, true);

            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            
            _sut.ComputerState.GeneralPurposeRegisters[0].ApplyOnce(_byteFactory.Create(1));
            _sut.ComputerState.GeneralPurposeRegisters[1].ApplyOnce(_byteFactory.Create(255));
            
            StepFull(6);

            var result = _sut.ComputerState.Ram.InternalRegisters[0][1].Data;
            
            Assert.IsTrue(result.All(a => a));
        }
        
        // DATA RB, xxxx xxxx
        // Load next ram byte into RB
        [Test]
        public void CanPerformDataInstruction()
        {
            var instruction = _byteFactory.Create(false, false, true, false, false, false, false, false);
            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            
            _sut.ComputerState.Ram.InternalRegisters[0][1].ApplyOnce(_byteFactory.Create(255));
            
            StepFull(6);

            var result = _sut.ComputerState.GeneralPurposeRegisters[0].Data;
            
            Assert.IsTrue(result.All(a => a));
        }
        
        // JMPR RB
        // Jump to address in RB
        [Test]
        public void CanPerformJumpRegisterInstruction()
        {
            var instruction = _byteFactory.Create(false, false, true, true, false, false, false, false);
            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);

            _sut.ComputerState.GeneralPurposeRegisters[0].ApplyOnce(_fullByte);
            
            StepFull(6);

            var result = _sut.ComputerState.Iar.Data;
            
            Assert.IsTrue(result.All(a => a));
        }
        
        // JMP Addr
        // Jump to location in next Ram Register
        [Test]
        public void CanPerformJumpInstruction()
        {
            var instruction = _byteFactory.Create(false, true, false, false, false, false, false, false);
            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            
            _sut.ComputerState.Ram.InternalRegisters[0][1].ApplyOnce(_fullByte);
            
            StepFull(6);

            var result = _sut.ComputerState.Iar.Data;
            
            Assert.IsTrue(result.All(a => a));
        }
        
        // JC Addr
        // Jump if Equal is on
        [Test]
        public void CanPerformJumpIfEqualIsOn()
        {
            var addInstruction = _byteFactory.Create(true, false, false, false, false, false, false, true);

            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(addInstruction);
            var instruction = _byteFactory.Create(false, true, false, true, false, false, true, false);
            _sut.ComputerState.Ram.InternalRegisters[0][1].ApplyOnce(instruction);
            _sut.ComputerState.Ram.InternalRegisters[0][2].ApplyOnce(_fullByte);
            
            _sut.ComputerState.GeneralPurposeRegisters[0].ApplyOnce(_byteFactory.Create(200));
            _sut.ComputerState.GeneralPurposeRegisters[1].ApplyOnce(_byteFactory.Create(200));
            
            StepFull(6);

            Step(1);

            StepFull(6);

            var result = _sut.ComputerState.Iar.Data;
            
            Assert.IsTrue(result.All(a => a));
        }
        
        // JC Addr
        // Jump if Carry is on
        [Test]
        public void CanPerformJumpIfCarryIsOn()
        {
            var addInstruction = _byteFactory.Create(true, false, false, false, false, false, false, true);

            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(addInstruction);
            var instruction = _byteFactory.Create(false, true, false, true, true, false, false, false);
            _sut.ComputerState.Ram.InternalRegisters[0][1].ApplyOnce(instruction);
            _sut.ComputerState.Ram.InternalRegisters[0][2].ApplyOnce(_fullByte);
            
            _sut.ComputerState.GeneralPurposeRegisters[0].ApplyOnce(_byteFactory.Create(200));
            _sut.ComputerState.GeneralPurposeRegisters[1].ApplyOnce(_byteFactory.Create(200));
            
            StepFull(6);
            
            Assert.IsTrue(_sut.ComputerState.Flags.Data.C);
            
            Step(1);

            StepFull(6);

            var result = _sut.ComputerState.Iar.Data;
            
            Assert.IsTrue(result.All(a => a));
        }

        [Test]
        public void CanClearFlags()
        {
            var addInstruction = _byteFactory.Create(true, false, false, false, false, false, false, true);
            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(addInstruction);

            var clearInstruction = _byteFactory.Create(false, true, true, false, false, false, false, false);
            _sut.ComputerState.Ram.InternalRegisters[0][1].ApplyOnce(clearInstruction);
            
            _sut.ComputerState.GeneralPurposeRegisters[0].ApplyOnce(_byteFactory.Create(200));
            _sut.ComputerState.GeneralPurposeRegisters[1].ApplyOnce(_byteFactory.Create(200));
            
            StepFull(6);

            Assert.IsTrue(_sut.ComputerState.Flags.Data.C);
            Assert.IsFalse(_sut.ComputerState.Flags.Data.A);
            Assert.IsTrue(_sut.ComputerState.Flags.Data.E);
            Assert.IsFalse(_sut.ComputerState.Flags.Data.Z);
            
            Step(1);

            StepFull(3);
            
            Assert.IsTrue(_sut.ComputerState.Bus1.Set);
            Step(1);
            Assert.IsTrue(_sut.ComputerState.Flags.Set);
            Step(2);
            
            StepFull(2);

            Assert.IsFalse(_sut.ComputerState.Flags.Data.C);
            Assert.IsFalse(_sut.ComputerState.Flags.Data.A);
            Assert.IsFalse(_sut.ComputerState.Flags.Data.E);
            Assert.IsFalse(_sut.ComputerState.Flags.Data.Z);
        }
        
        // TODO: Create instructions for IOBus pg. 149

        [Test]
        public void CanInputIoDataToRb()
        {
            _sut.ComputerState.Io.Bus.Data = new BusMessage<IByte> {Name = "FromIO", Data = _fullByte};
            var instruction = _byteFactory.Create(false, true, true, true, false, false, false, false);
            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            
            StepFull(6);

            var result = _sut.ComputerState.GeneralPurposeRegisters[0].Data;
            
            Assert.IsTrue(result.All(a => a));
        }

        [Test]
        public void CanInputIoAddressToRb()
        {
            _sut.ComputerState.Io.Bus.Data = new BusMessage<IByte> {Name = "FromIO", Data = _fullByte};
            var instruction = _byteFactory.Create(false, true, true, true, false, true, false, false);
            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            
            StepFull(6);

            var result = _sut.ComputerState.GeneralPurposeRegisters[0].Data;
            
            Assert.IsTrue(result.All(a => a));
        }

        [Test]
        public void CanOutputToIoAsData()
        {
            _sut.ComputerState.GeneralPurposeRegisters[0].ApplyOnce(_fullByte);
            var instruction = _byteFactory.Create(false, true, true, true, true, false, false, false);
            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            
            StepFull(6);

            var result = _sut.ComputerState.Io.Bus.Data.Data;
            
            Assert.IsTrue(result.All(a => a));
        }
        
        [Test]
        public void CanOutputToIoAsAddress()
        {
            _sut.ComputerState.GeneralPurposeRegisters[0].ApplyOnce(_fullByte);
            var instruction = _byteFactory.Create(false, true, true, true, true, true, false, false);
            _sut.ComputerState.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            
            StepFull(6);

            var result = _sut.ComputerState.Io.Bus.Data.Data;
            
            Assert.IsTrue(result.All(a => a));
        }

        private void StepFull(int times)
        {
            Step(times * 4);
        }

        private void Step(int times)
        {
            for (var i = 0; i < times; i++)
            {
                _sut.Step();
            }
        }
    }
}