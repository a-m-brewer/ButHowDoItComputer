using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class ComputerTests
    {
        private ByteFactory _byteFactory;
        private ByteToBase10Converter _byteToBase10;
        private Base10Converter _binaryConverter;

        [SetUp]
        public void Setup()
        {
            _byteFactory = TestUtils.CreateByteFactory();
            _byteToBase10 = TestUtils.CreateByteToBase10Converter();
            _binaryConverter = new Base10Converter();
        }

        [Test]
        public void ComputerFirstStepIsCorrect()
        {
            var sut = CreateSut();

            sut.Step();

            var bus1State = sut.Bus1.Set;
            var iarEnable = sut.InstructionAddressRegister.Enable;

            Assert.True(bus1State);
            Assert.True(iarEnable);
            
            sut.Step();
            
            var mar1Set = sut.Ram.MemoryAddressRegister.Set;
            var accSet = sut.Acc.Set;           
            
            Assert.True(mar1Set);
            Assert.True(accSet);
        }

        [Test]
        public void CpuSecondStepIsCorrect()
        {
            var sut = CreateSut();

            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
            
            sut.Step();
            
            var ramEnable = sut.Ram.Enable;

            Assert.True(ramEnable);

            sut.Step();
            
            var irSet = sut.InstructionRegister.Set;

            Assert.True(irSet);
        }

        [Test]
        public void CpuThirdStepIsCorrect()
        {
            var sut = CreateSut();

            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
            
            sut.Step();
            
            var accEnable = sut.Acc.Enable;

            Assert.True(accEnable);

            sut.Step();
            
            var iarSet = sut.InstructionAddressRegister.Set;

            Assert.True(iarSet);
        }

        [Test]
        public void AfterStep1ByteInIarIsInMar()
        {
            var sut = CreateSut();

            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(255));
            
            sut.Step();
            sut.Step();

            var result = sut.Ram.MemoryAddressRegister.Data;

            Assert.IsTrue(result.All(a => a));
        }

        [Test]
        public void AfterStep1TheAccIsIncrementedBy1()
        {
            var sut = CreateSut();
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            
            sut.Step();
            sut.Step();

            var result = sut.Acc.Data;
            
            Assert.IsTrue(result[0]);
        }

        [Test]
        public void AfterStep2ByteInRamIsPutIntoTheIr()
        {
            var sut = CreateSut();
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(_byteFactory.Create(255));
            
            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();

            var irResult = sut.InstructionRegister.Data;
            var irResultAllTrue = irResult.All(a => a);
            
            Assert.IsTrue(irResultAllTrue);
        }

        [Test]
        public void DuringStepOneTheAccSetPinIsEnabled()
        {
            var sut = CreateSut();
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            
            sut.Step();
            sut.Step();

            var result = sut.Acc.Set;
            
            Assert.IsTrue(result);
        }
        
        [Test]
        public void AfterStep3TheIncrementedAccIsPutIntoTheIar()
        {
            var sut = CreateSut();
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            
            Step(sut, 3);

            var result = sut.InstructionAddressRegister.Data;
            
            Assert.IsTrue(result[0]);
        }

        [Test]
        public void BeforeStep4TheInstructionInRamIsInTheIr()
        {
            var sut = CreateSut();
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(_byteFactory.Create(255));
            
            Step(sut, 3);
            
            var irResult = sut.InstructionRegister.Data;
            var irResultAllTrue = irResult.All(a => a);
            
            Assert.IsTrue(irResultAllTrue);
        }

        [Test]
        public void CorrectRegistersAreSelectedForAluAction()
        {
            var sut = CreateSut();

            var instructionBits = new[]
            {
                true, false, false, false, false, false, false,
                true
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);

            Step(sut, 3);
            sut.Step();
            var regBEnableState = sut.R1.Enable;
            Assert.IsTrue(regBEnableState);
            
            Step(sut, 1);

            var regAEnableState = sut.R0.Enable;
            Assert.IsTrue(regAEnableState);
            
            Step(sut, 1);
            sut.Step();

            var regBSet = sut.R1.Set;
            Assert.IsTrue(regBSet);
        }

        [Test]
        public void CanAddTwoItemsTogether()
        {
            var sut = CreateSut();

            var expected = _byteFactory.Create(15U);
            
            var instructionBits = new[]
            {
                true, false, false, false, false, false, false,
                true
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);
            sut.R0.ApplyOnce(_byteFactory.Create(10U));
            sut.R1.ApplyOnce(_byteFactory.Create(5U));

            Step(sut, 6);

            var result = sut.R1.Data;

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }
        
        [Test]
        public void CanAddTwoItemsTogetherFromDifferentRegisters()
        {
            var sut = CreateSut();

            var expected = _byteFactory.Create(15U);
            
            var instructionBits = new[]
            {
                true, false, false, false, true, false, true,
                true
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);
            sut.R2.ApplyOnce(_byteFactory.Create(10U));
            sut.R3.ApplyOnce(_byteFactory.Create(5U));

            Step(sut, 6);

            var result = sut.R3.Data;

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }

        [Test]
        public void CanShiftLeft()
        {
            var sut = CreateSut();

            var instructionBits = new[]
            {
                true, false, false, true, false, false, false, true
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);
            sut.R0.ApplyOnce(_byteFactory.Create(128));

            Step(sut, 6);

            var result = sut.R1.Data;

            Assert.IsTrue(result[6]);
        }
        
        [Test]
        public void CanShiftRight()
        {
            var sut = CreateSut();

            var instructionBits = new[]
            {
                true, false, true, false, false, false, false, true
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);
            sut.R0.ApplyOnce(_byteFactory.Create(1));

            Step(sut, 6);

            var result = sut.R1.Data;

            Assert.IsTrue(result[1]);
        }
        
        [Test]
        public void CpuCanNotByte()
        {
            var sut = CreateSut();

            var instructionBits = new[]
            {
                true, false, true, true, false, false, false, true
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);
            sut.R0.ApplyOnce(_byteFactory.Create(0));

            Step(sut, 6);

            var result = sut.R1.Data;

            Assert.IsTrue(result.All(a => a));
        }
        
        [Test]
        public void CpuCanAndByte()
        {
            var sut = CreateSut();

            var instructionBits = new[]
            {
                true, true, false, false, false, false, false, true
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);
            sut.R0.ApplyOnce(_byteFactory.Create(0));
            sut.R1.ApplyOnce(_byteFactory.Create(255));

            Step(sut, 6);

            var result = sut.R1.Data;

            Assert.IsTrue(result.All(a => !a));
        }
        
        [Test]
        public void CpuCanOrByte()
        {
            var sut = CreateSut();

            var instructionBits = new[]
            {
                true, true, false, true, false, false, false, true
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);
            sut.R0.ApplyOnce(_byteFactory.Create(255));
            sut.R1.ApplyOnce(_byteFactory.Create(0));

            Step(sut, 6);

            var result = sut.R1.Data;

            Assert.IsTrue(result.All(a => a));
        }
        
        [Test]
        public void CpuCanXOrByte()
        {
            var sut = CreateSut();

            var instructionBits = new[]
            {
                true, true, true, false, false, false, false, true
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);
            sut.R0.ApplyOnce(_byteFactory.Create(255));
            sut.R1.ApplyOnce(_byteFactory.Create(255));

            Step(sut, 6);

            var result = sut.R1.Data;

            Assert.IsTrue(result.All(a => !a));
        }
        
        [Test]
        public void CpuCanCompareByteEqual()
        {
            var sut = CreateSut();

            var instructionBits = new[]
            {
                true, true, true, true, false, false, false, true
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);
            sut.R0.ApplyOnce(_byteFactory.Create(255));
            sut.R1.ApplyOnce(_byteFactory.Create(255));

            Step(sut, 6);

            Assert.IsTrue(sut.ArithmeticLogicUnit.Output.Equal);
            Assert.IsFalse(sut.ArithmeticLogicUnit.Output.ALarger);
            Assert.IsFalse(sut.ArithmeticLogicUnit.Output.Output.All(a => a));
        }
        
        [Test]
        public void CpuCanCompareByteUnEqual()
        {
            var sut = CreateSut();

            var instructionBits = new[]
            {
                true, true, true, true, false, false, false, true
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);
            sut.R0.ApplyOnce(_byteFactory.Create(255));
            sut.R1.ApplyOnce(_byteFactory.Create(0));

            Step(sut, 6);

            Assert.IsFalse(sut.ArithmeticLogicUnit.Output.Equal);
            Assert.IsTrue(sut.ArithmeticLogicUnit.Output.ALarger);
            Assert.IsTrue(sut.ArithmeticLogicUnit.Output.Output.Any(a => a));
        }

        [Test]
        public void CanStoreARegisterByteInRam()
        {
            var sut = CreateSut();

            var instructionBits = new[]
            {
                false, false, false, false, false, false, false,
                true
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            // store the actual instruction in ram 0
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);
            
            // the value we want to load
            sut.Ram.InternalRegisters[0][1].ApplyOnce(_byteFactory.Create(255));
            
            // point ra to ram address 1
            sut.R0.ApplyOnce(_byteFactory.Create(1));
            
            Step(sut, 6);

            var result = sut.R1.Data;
            
            Assert.IsTrue(result.All(a => a));
        }

        [Test]
        public void CanStoreByteInRam()
        {
            var sut = CreateSut();

            var instructionBits = new[]
            {
                false, false, false, true, false, false, false,
                true
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            // store the actual instruction in ram 0
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);

            // ram address
            sut.R0.ApplyOnce(_byteFactory.Create(1));
            // data to store in ram
            sut.R1.ApplyOnce(_byteFactory.Create(255));
            
            Step(sut, 6);

            var result = sut.Ram.InternalRegisters[0][1].Data;
            
            Assert.IsTrue(result.All(a => a));
        }

        
        [Test]
        public void CanPerformTheDataInstruction()
        {
            var sut = CreateSut();

            var instruction = _byteFactory.Create(false, false, true, false, false, false, false, false);
            
            // store the actual instruction in ram 0
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instruction);

            sut.Ram.InternalRegisters[0][1].ApplyOnce(_byteFactory.Create(255));
            
            Step(sut, 6);

            var result = sut.R0.Data;
            
            Assert.IsTrue(result.All(a => a));
        }

        [Test]
        public void CanPerformAJumpRegisterInstruction()
        {
            var sut = CreateSut();

            var instruction = _byteFactory.Create(false, false, true, true, false, false, false, false);
            
            // store the actual instruction in ram 0
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            
            sut.R0.ApplyOnce(_byteFactory.Create(255));
            
            Step(sut, 6);

            var result = sut.InstructionAddressRegister.Data;
            
            Assert.IsTrue(result.All(a => a));
        }

        [Test]
        public void CanPerformJumpInstruction()
        {
            var sut = CreateSut();

            var instruction = _byteFactory.Create(false, true, false, false, false, false, false, false);
            
            // store the actual instruction in ram 0
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instruction);
            sut.Ram.InternalRegisters[0][1].ApplyOnce(_byteFactory.Create(255));
            
            Step(sut, 6);

            var result = sut.InstructionAddressRegister.Data;
            
            Assert.IsTrue(result.All(a => a));
        }
        
        // TODO: Make tests for the third great invention pg. 132

        [Test]
        public void WillJumpIfCarryIsSetOnPreviousInstruction()
        {
            var sut = CreateSut();

            var expected = _byteFactory.Create(255U);
            
            var instructionBits = new[]
            {
                true, false, false, false, false, false, false, true
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);
            sut.R0.ApplyOnce(_byteFactory.Create(true, true, true, true, true, true, true, true));
            sut.R1.ApplyOnce(_byteFactory.Create(true, true, true, true, true, true, true, true));
            
            // setup for the jump if
            var jumpIfCarryOnInstruction = _byteFactory.Create(false, true, false, true, true, false, false, false);
            sut.Ram.InternalRegisters[0][1].ApplyOnce(jumpIfCarryOnInstruction);
            sut.Ram.InternalRegisters[0][2].ApplyOnce(_byteFactory.Create(255));

            Step(sut, 6);

            var addrRes = sut.R1.Data;

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], addrRes[i]);
            }

            Assert.IsTrue(sut.CaezRegister.Data.C);
            Assert.IsFalse(sut.CaezRegister.Data.A);
            Assert.IsTrue(sut.CaezRegister.Data.E);
            Assert.IsFalse(sut.CaezRegister.Data.Z);
     
            Step(sut, 3);
            sut.Step();
            
            Assert.IsTrue(sut.Bus1.Set);
            Assert.IsTrue(sut.InstructionAddressRegister.Enable);
            
            sut.Step();

            Assert.IsTrue(sut.Ram.MemoryAddressRegister.Set);
            Assert.IsTrue(sut.Acc.Set);
            
            sut.Step();
            sut.Step();
            
            sut.Step();
            
            Assert.IsTrue(sut.Acc.Enable);
            
            sut.Step(); 
            
            Assert.IsTrue(sut.InstructionAddressRegister.Set);
            
            sut.Step();
            sut.Step();
            
            sut.Step();
            
            // Seems that the CAEZ is false false true false
            Assert.IsTrue(sut.Ram.Enable);
            
            sut.Step();
            
            Assert.IsTrue(sut.InstructionAddressRegister.Set);

            sut.Step();
            sut.Step();
            
            sut.Step();
            
            // TODO: appears to be jumping to ram reg 129?
            // TODO: 129 appears to be the add instruction in binary

            var result = sut.InstructionAddressRegister.Data;
            //
            Assert.IsTrue(result.All(a => a));
        }

        [Test]
        public void CanClearAllFlags()
        {
            var sut = CreateSut();
            
            var addInstruction = _byteFactory.Create(true, false, false, false, false, false, false, true);
            
            // setup for add instruction that will make a carry on bit be set
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(addInstruction);
            sut.R0.ApplyOnce(_byteFactory.Create(200U));
            sut.R1.ApplyOnce(_byteFactory.Create(56U));
            
            var clearFlagsInstruction = _byteFactory.Create(false, true, true, false, false, false, false, false);
            sut.Ram.InternalRegisters[0][1].ApplyOnce(clearFlagsInstruction);
            
            Step(sut, 6);
            sut.Step();
            Step(sut, 6);

            Assert.IsFalse(sut.CaezRegister.Data.C);
            Assert.IsFalse(sut.CaezRegister.Data.A);
            Assert.IsFalse(sut.CaezRegister.Data.E);
            Assert.IsFalse(sut.CaezRegister.Data.Z);
        }        

        private void Step(Computer sut, int times)
        {
            for (int i = 0; i < times; i++)
            {
                sut.Step();
                sut.Step();
                sut.Step();
                sut.Step();
            }
        }

        private static Computer CreateSut() => new Computer(TestUtils.CreateArithmeticLogicUnit());
    }
}