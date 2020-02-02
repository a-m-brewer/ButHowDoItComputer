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
        
        //[Test]
        
        // public void CorrectRegistersAreSelectedBaseUponAluIr(uint ra, uint rb)
        // {
        //     var registerA = ra.ToBit().Pad(2).ToArray();
        //     var registerB = rb.ToBit().Pad(2).ToArray();
        //     var instructionBits = new[]
        //     {
        //         registerB[0], registerB[1], registerA[0], registerA[1], false.ToBit(), false.ToBit(), false.ToBit(),
        //         true.ToBit()
        //     };
        //     var instruction = _byteFactory.Create(instructionBits);
        //     
        //     
        // }

        private void SkipToStartOfStep4(Computer sut)
        {
            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
            sut.Step();
        }

        private static Computer CreateSut() => new Computer(TestUtils.CreateArithmeticLogicUnit());
    }
}