using System.Linq;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class ComputerTests
    {
        private ByteFactory _byteFactory;
        private ByteToBase10Converter _byteToBase10;

        [SetUp]
        public void Setup()
        {
            _byteFactory = TestUtils.CreateByteFactory();
            _byteToBase10 = TestUtils.CreateByteToBase10Converter();
        }

        [Test]
        public void ComputerFirstStepIsCorrect()
        {
            var sut = CreateSut();

            sut.Step();

            var bus1State = sut.Bus1.Set.State;
            var iarEnable = sut.InstructionAddressRegister.Enable.State;

            Assert.True(bus1State);
            Assert.True(iarEnable);
            
            sut.Step();
            
            var mar1Set = sut.Ram.MemoryAddressRegister.Set.State;
            var accSet = sut.Acc.Set.State;           
            
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
            
            var ramEnable = sut.Ram.Enable.State;

            Assert.True(ramEnable);

            sut.Step();
            
            var irSet = sut.InstructionRegister.Set.State;

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
            
            var accEnable = sut.Acc.Enable.State;

            Assert.True(accEnable);

            sut.Step();
            
            var iarSet = sut.InstructionAddressRegister.Set.State;

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

            Assert.IsTrue(result.All(a => a.State));
        }

        [Test]
        public void AfterStep1TheAccIsIncrementedBy1()
        {
            var sut = CreateSut();
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            
            sut.Step();
            sut.Step();

            var result = sut.Acc.Data;
            
            Assert.IsTrue(result[0].State);
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
            var irResultAllTrue = irResult.All(a => a.State);
            
            Assert.IsTrue(irResultAllTrue);
        }

        [Test]
        public void DuringStepOneTheAccSetPinIsEnabled()
        {
            var sut = CreateSut();
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            
            sut.Step();
            sut.Step();

            var result = sut.Acc.Set.State;
            
            Assert.IsTrue(result);
        }
        
        [Test]
        public void AfterStep3TheIncrementedAccIsPutIntoTheIar()
        {
            var sut = CreateSut();
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            
            Step(sut, 3);

            var result = sut.InstructionAddressRegister.Data;
            
            Assert.IsTrue(result[0].State);
        }

        [Test]
        public void BeforeStep4TheInstructionInRamIsInTheIr()
        {
            var sut = CreateSut();
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(_byteFactory.Create(255));
            
            Step(sut, 3);
            
            var irResult = sut.InstructionRegister.Data;
            var irResultAllTrue = irResult.All(a => a.State);
            
            Assert.IsTrue(irResultAllTrue);
        }

        [Test]
        public void CorrectRegistersAreSelectedForAluAction()
        {
            var sut = CreateSut();

            var instructionBits = new[]
            {
                true.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(),
                true.ToBit()
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);

            Step(sut, 3);
            sut.Step();
            var regBEnableState = sut.R1.Enable.State;
            Assert.IsTrue(regBEnableState);
            
            Step(sut, 1);

            var regAEnableState = sut.R0.Enable.State;
            Assert.IsTrue(regAEnableState);
            
            Step(sut, 1);
            sut.Step();

            var regBSet = sut.R1.Set.State;
            Assert.IsTrue(regBSet);
        }

        [Test]
        public void CanAddTwoItemsTogether()
        {
            var sut = CreateSut();

            var expected = _byteFactory.Create(15U);
            
            var instructionBits = new[]
            {
                true.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(),
                true.ToBit()
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
                Assert.AreEqual(expected[i].State, result[i].State);
            }
        }

        [Test]
        public void CanShiftLeft()
        {
            var sut = CreateSut();

            var instructionBits = new[]
            {
                true.ToBit(), false.ToBit(), false.ToBit(), true.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), true.ToBit()
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);
            sut.R0.ApplyOnce(_byteFactory.Create(128));

            Step(sut, 6);

            var result = sut.R1.Data;

            Assert.IsTrue(result[6].State);
        }
        
        [Test]
        public void CanShiftRight()
        {
            var sut = CreateSut();

            var instructionBits = new[]
            {
                true.ToBit(), false.ToBit(), true.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), false.ToBit(), true.ToBit()
            };
            var instructionByte = _byteFactory.Create(instructionBits);
            
            sut.InstructionAddressRegister.ApplyOnce(_byteFactory.Create(0));
            sut.Ram.InternalRegisters[0][0].ApplyOnce(instructionByte);
            sut.R0.ApplyOnce(_byteFactory.Create(1));

            Step(sut, 6);

            var result = sut.R1.Data;

            Assert.IsTrue(result[1].State);
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