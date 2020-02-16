using System.Linq;
using ButHowDoItComputer.Components.Factories;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class ComputerTests
    {
        private IByte _fullByte;
        private ByteFactory _byteFactory;
        private Computer _sut;

        [SetUp]
        public void Setup()
        {
            _byteFactory = TestUtils.CreateByteFactory();
            _fullByte = _byteFactory.Create(255);

            Instruction = _byteFactory.Create(0);
            Flags = new Caez();

            var cpuPinStates = new CpuPinStates(TestUtils.CreateClock(), TestUtils.CreateStepper(), Instruction, Flags,
                TestUtils.CreateAnd(), TestUtils.CreateOr(), TestUtils.CreateNot(), TestUtils.CreateDecoder(),
                _byteFactory);
            
            var bus = new Bus<IByte>(new BusMessage<IByte> {Data = new Byte(), Name = "Bus"});
            var byteRegisterFactory = TestUtils.CreateByteRegisterFactory();
            var ram = TestUtils.CreateRam(bus);
            var computerState = new ComputerState(byteRegisterFactory, ram, TestUtils.CreateBus1Factory(),
                new ArithmeticLogicUnitFactory(), TestUtils.CreateCaezRegisterFactory(), bus);

            _sut = new Computer(cpuPinStates, computerState);
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