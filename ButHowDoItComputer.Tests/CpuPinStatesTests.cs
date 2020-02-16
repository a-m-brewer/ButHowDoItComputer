using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Parts;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    public class CpuPinStatesTests
    {
        private Clock _clock;
        private Stepper _stepper;
        private CpuPinStates _sut;
        private IByte Instruction { get; set; }
        private Caez Caez { get; set; }

        [SetUp]
        public void Setup()
        {
            _clock = TestUtils.CreateClock();
            _stepper = TestUtils.CreateStepper();
            Instruction = new Byte();
            Caez = new Caez();
            _sut = new CpuPinStates(_clock, _stepper, Instruction, Caez, new And(), TestUtils.CreateOr(), TestUtils.CreateNot(),
                TestUtils.CreateDecoder(), TestUtils.CreateByteFactory());
        }

        // STEP 1

        // ENABLE

        [Test]
        public void AfterOneStepEnableIsOn()
        {
            var result = _sut.Step();
            Assert.IsTrue(result.ClockOutput.ClkE);
        }

        [Test]
        public void AfterOneStepStepperIsOnStep1()
        {
            var result = _sut.Step();
            Assert.IsTrue(result.StepperOutput[0]);
        }

        [Test]
        public void DuringStep1EnableBus1SetIsUpdatedToTrue()
        {
            var result = _sut.Step();
            Assert.IsTrue(result.Bus1);
        }

        [Test]
        public void DuringStep1EnableIarEnableIsTrue()
        {
            var result = _sut.Step();
            Assert.IsTrue(result.Iar.Enable);
        }

        // Sets

        [Test]
        public void AfterOneSetStepEnableIsOn()
        {
            var result = Step(2);
            Assert.IsTrue(result.ClockOutput.ClkS);
        }

        [Test]
        public void AfterOneSetStepStepperIsOnStep1()
        {
            var result = Step(2);
            Assert.IsTrue(result.StepperOutput[0]);
        }

        [Test]
        public void AfterStepOneSetMarIsSet()
        {
            var result = Step(2);
            Assert.IsTrue(result.Mar);
        }

        [Test]
        public void AfterStepOneSetAccIsSet()
        {
            var result = Step(2);
            Assert.IsTrue(result.Acc.Set);
        }

        // Step 2

        // Enables

        [Test]
        public void AfterTwoStepEnableIsOn()
        {
            StepFull(1);
            var result = Step(1);
            Assert.IsTrue(result.ClockOutput.ClkE);
        }

        [Test]
        public void AfterTwoStepStepperIsOnStep2()
        {
            StepFull(1);
            var result = Step(1);
            Assert.IsTrue(result.StepperOutput[1]);
        }

        [Test]
        public void AfterTwoEnableRamEnableIsOn()
        {
            StepFull(1);
            var result = Step(1);
            Assert.IsTrue(result.Ram.Enable);
        }

        // Sets

        [Test]
        public void AfterTwoStepSetIsOn()
        {
            StepFull(1);
            var result = Step(2);
            Assert.IsTrue(result.ClockOutput.ClkS);
        }

        [Test]
        public void AfterTwoStepSetStepperIsOnStep2()
        {
            StepFull(1);
            var result = Step(2);
            Assert.IsTrue(result.StepperOutput[1]);
        }

        [Test]
        public void AfterStep2SetIrSetIsTrue()
        {
            StepFull(1);
            var result = Step(2);
            Assert.IsTrue(result.Ir.Set);
        }

        // Step 3

        // Enables

        [Test]
        public void AfterThreeStepEnableIsOn()
        {
            StepFull(2);
            var result = Step(1);
            Assert.IsTrue(result.ClockOutput.ClkE);
        }

        [Test]
        public void AfterThreeStepEnableStepperIsOnStep3()
        {
            StepFull(2);
            var result = Step(1);
            Assert.IsTrue(result.StepperOutput[2]);
        }

        [Test]
        public void AfterThreeEnableStepAccEnableIsTrue()
        {
            StepFull(2);
            var result = Step(1);
            Assert.IsTrue(result.Acc.Enable);
        }

        // Sets

        [Test]
        public void AfterThreeStepSetIsOn()
        {
            StepFull(2);
            var result = Step(2);
            Assert.IsTrue(result.ClockOutput.ClkS);
        }

        [Test]
        public void AfterThreeStepSetStepperIsOnStep3()
        {
            StepFull(2);
            var result = Step(2);
            Assert.IsTrue(result.StepperOutput[2]);
        }

        [Test]
        public void AfterThreeSetStepIarSetIsTrue()
        {
            StepFull(2);
            var result = Step(2);
            Assert.IsTrue(result.Iar.Set);
        }

        // Step 4

        // Enable

        [Test]
        public void AfterFourStepEnableIsOn()
        {
            StepFull(3);
            var result = Step(1);
            Assert.IsTrue(result.ClockOutput.ClkE);
        }

        [Test]
        public void AfterFourStepEnableStepperIsOnStep4()
        {
            StepFull(3);
            var result = Step(1);
            Assert.IsTrue(result.StepperOutput[3]);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void AfterFourEnableStepRegBEnableIsTrue(bool aluFlag)
        {
            Instruction[0] = aluFlag;
            StepFull(3);
            var result = Step(1);
            Assert.AreEqual(aluFlag, result.RegB.Enable);
        }

        [Test]
        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void AfterFourEnableStepRegAIsEnabled(bool aluFlag, bool decoder)
        {
            Instruction[0] = aluFlag;
            Instruction[3] = decoder;

            StepFull(3);
            var result = Step(1);

            Assert.AreEqual(!aluFlag, result.RegA.Enable);
        }

        [Test]
        public void AfterStep4EnableDataInstructionBus1IsSet()
        {
            Instruction[2] = true;

            StepFull(3);
            var result = Step(1);

            Assert.IsTrue(result.Bus1);
        }

        [Test]
        public void AfterStep4EnableDataInstructionIarIsTrue()
        {
            Instruction[2] = true;

            StepFull(3);
            var result = Step(1);

            Assert.IsTrue(result.Iar.Enable);
        }

        [Test]
        public void AfterStep4EnableJumpRegisterInstructionRegBIsTrue()
        {
            Instruction[2] = true;
            Instruction[3] = true;

            StepFull(3);
            var result = Step(1);

            Assert.IsTrue(result.RegB.Enable);
        }

        [Test]
        public void AfterStep4EnableJumpIarIsTrue()
        {
            Instruction[1] = true;

            StepFull(3);
            var result = Step(1);

            Assert.IsTrue(result.Iar.Enable);
        }

        [Test]
        public void AfterStep4EnableJumpIfBus1IsTrue()
        {
            Instruction[1] = true;
            Instruction[3] = true;

            StepFull(3);
            var result = Step(1);

            Assert.IsTrue(result.Bus1);
        }

        [Test]
        public void AfterStep4EnableJumpIfIarIsTrue()
        {
            Instruction[1] = true;
            Instruction[3] = true;

            StepFull(3);
            var result = Step(1);

            Assert.IsTrue(result.Iar.Enable);
        }

        [Test]
        public void AfterStep4EnableClearBus1IsTrue()
        {
            Instruction[1] = true;
            Instruction[2] = true;

            StepFull(3);
            var result = Step(1);
            
            Assert.IsTrue(result.Bus1);
        }

        // Sets

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void AfterFourSetStepTmpSetIsTrue(bool aluFlag)
        {
            Instruction[0] = aluFlag;
            StepFull(3);
            var result = Step(2);
            Assert.AreEqual(aluFlag, result.Tmp.Set);
        }
        
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void AfterFourSetStepCarryInTmpSetIsTrue(bool aluFlag)
        {
            Instruction[0] = aluFlag;
            StepFull(3);
            var result = Step(2);
            Assert.AreEqual(aluFlag, result.CarryInTmp);
        }

        [Test]
        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void AfterFourSetMarIsSet(bool aluFlag, bool decoder)
        {
            Instruction[0] = aluFlag;
            Instruction[3] = decoder;

            StepFull(3);
            var result = Step(2);

            Assert.AreEqual(!aluFlag, result.Mar);
        }

        [Test]
        public void AfterStep4SetDataInstructionMarIsTrue()
        {
            Instruction[2] = true;

            StepFull(3);
            var result = Step(2);

            Assert.IsTrue(result.Mar);
        }

        [Test]
        public void AfterStep4SetDataInstructionAccIsTrue()
        {
            Instruction[2] = true;

            StepFull(3);
            var result = Step(2);

            Assert.IsTrue(result.Acc.Set);
        }

        [Test]
        public void AfterStep4SetJumpRegisterInstructionIarIsTrue()
        {
            Instruction[2] = true;
            Instruction[3] = true;

            StepFull(3);
            var result = Step(2);

            Assert.IsTrue(result.Iar.Set);
        }

        [Test]
        public void AfterStep4SetJumpMarIsTrue()
        {
            Instruction[1] = true;

            StepFull(3);
            var result = Step(2);

            Assert.IsTrue(result.Mar);
        }

        [Test]
        public void AfterStep4SetJumpIfMarIsTrue()
        {
            Instruction[1] = true;
            Instruction[3] = true;

            StepFull(3);
            var result = Step(2);

            Assert.IsTrue(result.Mar);
        }

        [Test]
        public void AfterStep4SetJumpIfAccIsTrue()
        {
            Instruction[1] = true;
            Instruction[3] = true;

            StepFull(3);
            var result = Step(2);

            Assert.IsTrue(result.Acc.Set);
        }
        
        [Test]
        public void AfterStep4SetClearCaezFlagsIsTrue()
        {
            Instruction[1] = true;
            Instruction[2] = true;

            StepFull(3);
            var result = Step(2);
            
            Assert.IsTrue(result.Flags);
        }

        // Step 5

        // enable

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void AfterStep5RegAEnableIsTrue(bool aluFlag)
        {
            Instruction[0] = aluFlag;
            StepFull(4);
            var result = Step(1);
            Assert.AreEqual(aluFlag, result.RegA.Enable);
        }

        [Test]
        [TestCase(false, false, true, false)]
        [TestCase(false, true, false, true)]
        [TestCase(true, false, false, false)]
        [TestCase(true, true, false, false)]
        public void AfterStepFiveEnableEitherRamOrRegBIsSet(bool aluFlag, bool decoder, bool ramSet, bool rbSet)
        {
            Instruction[0] = aluFlag;
            Instruction[3] = decoder;

            StepFull(4);
            var result = Step(1);

            Assert.AreEqual(ramSet, result.Ram.Enable);
            Assert.AreEqual(rbSet, result.RegB.Enable);
        }

        [Test]
        public void AfterStep5EnableDataInstructionRamIsTrue()
        {
            Instruction[2] = true;

            StepFull(4);
            var result = Step(1);

            Assert.IsTrue(result.Ram.Enable);
        }

        [Test]
        public void AfterStep5EnableJumpRamIsTrue()
        {
            Instruction[1] = true;

            StepFull(4);
            var result = Step(1);

            Assert.IsTrue(result.Ram.Enable);
        }

        [Test]
        public void AfterStep5JumpIfEnableAccIsTrue()
        {
            Instruction[1] = true;
            Instruction[3] = true;

            StepFull(4);
            var result = Step(1);

            Assert.IsTrue(result.Acc.Enable);
        }

        // set

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void AfterStep5AccSetIsTrue(bool aluFlag)
        {
            Instruction[0] = aluFlag;
            StepFull(4);
            var result = Step(2);
            Assert.AreEqual(aluFlag, result.Acc.Set);
        }

        [Test]
        [TestCase(false, false, false, true)]
        [TestCase(false, true, true, false)]
        [TestCase(true, false, false, false)]
        [TestCase(true, true, false, false)]
        public void AfterStepFiveSetEitherRamOrRegBIsSet(bool aluFlag, bool decoder, bool ramSet, bool rbSet)
        {
            Instruction[0] = aluFlag;
            Instruction[3] = decoder;

            StepFull(4);
            var result = Step(2);

            Assert.AreEqual(ramSet, result.Ram.Set);
            Assert.AreEqual(rbSet, result.RegB.Set);
        }

        [Test]
        public void AfterStep5SetDataInstructionRegBIsTrue()
        {
            Instruction[2] = true;

            StepFull(4);
            var result = Step(2);

            Assert.IsTrue(result.RegB.Set);
        }

        [Test]
        public void AfterStep5SetJumpIarIsTrue()
        {
            Instruction[1] = true;

            StepFull(4);
            var result = Step(2);

            Assert.IsTrue(result.Iar.Set);
        }

        [Test]
        public void AfterStep5JumpIfSetIarIsTrue()
        {
            Instruction[1] = true;
            Instruction[3] = true;

            StepFull(4);
            var result = Step(2);

            Assert.IsTrue(result.Iar.Set);
        }

        [Test]
        public void AfterStep5IfAluOpFlagsSetIsTrue()
        {
            Instruction[0] = true;
            StepFull(4);
            var result = Step(2);
            
            Assert.IsTrue(result.Flags);
        }

        // ALU Op

        [Test]
        [TestCase(false, false, false, false, false, false, false)]
        [TestCase(false, false, false, true, false, false, false)]
        [TestCase(false, false, true, false, false, false, false)]
        [TestCase(false, false, true, true, false, false, false)]
        [TestCase(false, true, false, false, false, false, false)]
        [TestCase(false, true, false, true, false, false, false)]
        [TestCase(false, true, true, false, false, false, false)]
        [TestCase(false, true, true, true, false, false, false)]
        [TestCase(true, false, false, false, false, false, false)]
        [TestCase(true, false, false, true, false, false, true)]
        [TestCase(true, false, true, false, false, true, false)]
        [TestCase(true, false, true, true, false, true, true)]
        [TestCase(true, true, false, false, true, false, false)]
        [TestCase(true, true, false, true, true, false, true)]
        [TestCase(true, true, true, false, true, true, false)]
        [TestCase(true, true, true, true, true, true, true)]
        public void CorrectOpCodeIsSetOnStep5(bool aluFlag, bool ir1, bool ir2, bool ir3, bool expected1,
            bool expected2, bool expected3)
        {
            Instruction[0] = aluFlag;
            Instruction[1] = ir1;
            Instruction[2] = ir2;
            Instruction[3] = ir3;

            StepFull(4);
            var result = Step(1);

            Assert.AreEqual(expected1, result.Op.One);
            Assert.AreEqual(expected2, result.Op.Two);
            Assert.AreEqual(expected3, result.Op.Three);
        }

        // Step 6

        // enable

        [Test]
        [TestCase(false, false, false, false, false)]
        [TestCase(false, false, false, true, false)]
        // this case is actually the data instruction
        // [TestCase(false, false, true, false, false)]
        [TestCase(false, false, true, true, false)]
        [TestCase(false, true, false, false, false)]
        [TestCase(false, true, false, true, false)]
        [TestCase(false, true, true, false, false)]
        [TestCase(false, true, true, true, false)]
        [TestCase(true, false, false, false, true)]
        [TestCase(true, false, false, true, true)]
        [TestCase(true, false, true, false, true)]
        [TestCase(true, false, true, true, true)]
        [TestCase(true, true, false, false, true)]
        [TestCase(true, true, false, true, true)]
        [TestCase(true, true, true, false, true)]
        [TestCase(true, true, true, true, false)]
        public void AfterStep6EnableAccIsEnabled(bool aluFlag, bool ir1, bool ir2, bool ir3, bool expected)
        {
            Instruction[0] = aluFlag;
            Instruction[1] = ir1;
            Instruction[2] = ir2;
            Instruction[3] = ir3;

            StepFull(5);
            var result = Step(1);
            Assert.AreEqual(expected, result.Acc.Enable);
        }

        [Test]
        public void AfterStep6EnableDataInstructionAccIsTrue()
        {
            Instruction[2] = true;

            StepFull(5);
            var result = Step(1);

            Assert.IsTrue(result.Acc.Enable);
        }

        [Test]
        [TestCase(false, false, false, false, false, false, false, false, false)]
        [TestCase(false, false, false, false, false, false, false, true, false)]
        [TestCase(false, false, false, true, false, false, false, false, false)]
        [TestCase(false, false, false, true, false, false, false, true, true)]
        [TestCase(false, false, false, false, false, false, false, false, false)]
        [TestCase(false, false, false, false, false, false, true, false, false)]
        [TestCase(false, false, true, false, false, false, false, false, false)]
        [TestCase(false, false, true, false, false, false, true, false, true)]
        [TestCase(false, false, false, false, false, false, false, false, false)]
        [TestCase(false, false, false, false, false, true, false, false, false)]
        [TestCase(false, true, false, false, false, false, false, false, false)]
        [TestCase(false, true, false, false, false, true, false, false, true)]
        [TestCase(false, false, false, false, false, false, false, false, false)]
        [TestCase(false, false, false, false, true, false, false, false, false)]
        [TestCase(true, false, false, false, false, false, false, false, false)]
        [TestCase(true, false, false, false, true, false, false, false, true)]
        public void AfterStep6EnableJumpIfRamIsTrue(bool c, bool a, bool e, bool z, bool cCheck, bool aCheck,
            bool eCheck, bool zCheck, bool expected)
        {
            Instruction[1] = true;
            Instruction[3] = true;

            Instruction[4] = cCheck;
            Instruction[5] = aCheck;
            Instruction[6] = eCheck;
            Instruction[7] = zCheck;

            Caez.C = c;
            Caez.A = a;
            Caez.E = e;
            Caez.Z = z;

            StepFull(5);
            var result = Step(1);

            Assert.AreEqual(expected, result.Ram.Enable);
        }

        // set

        [Test]
        [TestCase(false, false, false, false, false)]
        [TestCase(false, false, false, true, false)]
        [TestCase(false, false, true, false, false)]
        [TestCase(false, false, true, true, false)]
        [TestCase(false, true, false, false, false)]
        [TestCase(false, true, false, true, false)]
        [TestCase(false, true, true, false, false)]
        [TestCase(false, true, true, true, false)]
        [TestCase(true, false, false, false, true)]
        [TestCase(true, false, false, true, true)]
        [TestCase(true, false, true, false, true)]
        [TestCase(true, false, true, true, true)]
        [TestCase(true, true, false, false, true)]
        [TestCase(true, true, false, true, true)]
        [TestCase(true, true, true, false, true)]
        [TestCase(true, true, true, true, false)]
        public void AfterStep6SetRegBIsSet(bool aluFlag, bool ir1, bool ir2, bool ir3, bool expected)
        {
            Instruction[0] = aluFlag;
            Instruction[1] = ir1;
            Instruction[2] = ir2;
            Instruction[3] = ir3;

            StepFull(5);
            var result = Step(2);
            Assert.AreEqual(expected, result.RegB.Set);
        }

        [Test]
        public void AfterStep6SetDataInstructionIarIsTrue()
        {
            Instruction[2] = true;

            StepFull(5);
            var result = Step(2);

            Assert.IsTrue(result.Iar.Set);
        }

        [Test]
        [TestCase(false, false, false, false, false, false, false, false, false)]
        [TestCase(false, false, false, false, false, false, false, true, false)]
        [TestCase(false, false, false, true, false, false, false, false, false)]
        [TestCase(false, false, false, true, false, false, false, true, true)]
        [TestCase(false, false, false, false, false, false, false, false, false)]
        [TestCase(false, false, false, false, false, false, true, false, false)]
        [TestCase(false, false, true, false, false, false, false, false, false)]
        [TestCase(false, false, true, false, false, false, true, false, true)]
        [TestCase(false, false, false, false, false, false, false, false, false)]
        [TestCase(false, false, false, false, false, true, false, false, false)]
        [TestCase(false, true, false, false, false, false, false, false, false)]
        [TestCase(false, true, false, false, false, true, false, false, true)]
        [TestCase(false, false, false, false, false, false, false, false, false)]
        [TestCase(false, false, false, false, true, false, false, false, false)]
        [TestCase(true, false, false, false, false, false, false, false, false)]
        [TestCase(true, false, false, false, true, false, false, false, true)]
        public void AfterStep6SetJumpIfIarIsTrue(bool c, bool a, bool e, bool z, bool cCheck, bool aCheck, bool eCheck,
            bool zCheck, bool expected)
        {
            Instruction[1] = true;
            Instruction[3] = true;

            Instruction[4] = cCheck;
            Instruction[5] = aCheck;
            Instruction[6] = eCheck;
            Instruction[7] = zCheck;

            Caez.C = c;
            Caez.A = a;
            Caez.E = e;
            Caez.Z = z;

            StepFull(5);
            var result = Step(2);

            Assert.AreEqual(expected, result.Iar.Set);
        }

        // Other

        // Reg select

        [Test]
        [TestCase(false, false, false, false, 0, 0)]
        [TestCase(false, false, false, true, 0, 1)]
        [TestCase(false, false, true, false, 0, 2)]
        [TestCase(false, false, true, true, 0, 3)]
        [TestCase(false, true, false, false, 1, 0)]
        [TestCase(false, true, false, true, 1, 1)]
        [TestCase(false, true, true, false, 1, 2)]
        [TestCase(false, true, true, true, 1, 3)]
        [TestCase(true, false, false, false, 2, 0)]
        [TestCase(true, false, false, true, 2, 1)]
        [TestCase(true, false, true, false, 2, 2)]
        [TestCase(true, false, true, true, 2, 3)]
        [TestCase(true, true, false, false, 3, 0)]
        [TestCase(true, true, false, true, 3, 1)]
        [TestCase(true, true, true, false, 3, 2)]
        [TestCase(true, true, true, true, 3, 3)]
        public void CanUpdateEnableFlagForGeneralPurposeRegisters(bool ir4, bool ir5, bool ir6, bool ir7, int eReg,
            int sReg)
        {
            Instruction[4] = ir4;
            Instruction[5] = ir5;

            Instruction[6] = ir6;
            Instruction[7] = ir7;

            var result = Step(1);
            _sut.UpdateGeneralPurposeRegisters(true, true, true);
            Assert.IsTrue(result.GeneralPurposeRegisters[eReg].Enable);

            result = Step(1);
            _sut.UpdateGeneralPurposeRegisters(true, true, true);
            Assert.IsTrue(result.GeneralPurposeRegisters[sReg].Set);
        }

        private PinStates Step(int times)
        {
            var ps = new PinStates();
            for (var i = 0; i < times; i++) ps = _sut.Step();

            return ps;
        }

        private PinStates StepFull(int times)
        {
            return Step(times * 4);
        }
    }
}