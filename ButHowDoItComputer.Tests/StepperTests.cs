using System.Linq;
using ButHowDoItComputer.DataTypes;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class StepperTests
    {
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void StepperWorksAsExpected(int onBit)
        {
            var stepper = TestUtils.CreateStepper();

            var stepperOutput = new StepperOutput(TestUtils.CreateBitFactory());

            for (var i = 0; i <= onBit; i++)
            {
                stepper.Step(new Bit(true), new Bit(false));
                stepperOutput = stepper.Step(new Bit(false), new Bit(false));
            }

            Assert.IsTrue(stepperOutput[onBit].State);
            var rest = stepperOutput.Where(w => stepperOutput.IndexOf(w) != onBit);
            var restOn = rest.Any(w => w.State);
            Assert.IsFalse(restOn);
        }

        [Test]
        public void StaysAtIndex7IfNotReset()
        {
            var stepper = TestUtils.CreateStepper();

            var stepperOutput = new StepperOutput(TestUtils.CreateBitFactory());

            for (var i = 0; i < 8; i++)
            {
                stepper.Step(new Bit(true), new Bit(false));
                stepperOutput = stepper.Step(new Bit(false), new Bit(false));
            }

            Assert.IsTrue(stepperOutput[6].State);
            var rest = stepperOutput.Where(w => stepperOutput.IndexOf(w) != 6);
            var restOn = rest.Any(w => w.State);
            Assert.IsFalse(restOn);
        }

        [Test]
        public void CanResetTo0()
        {
            var stepper = TestUtils.CreateStepper();

            var stepperOutput = new StepperOutput(TestUtils.CreateBitFactory());

            for (var i = 0; i < 8; i++)
            {
                stepper.Step(new Bit(true), new Bit(false));
                stepperOutput = stepper.Step(new Bit(false), new Bit(false));
            }

            Assert.IsTrue(stepperOutput[6].State);
            var rest = stepperOutput.Where(w => stepperOutput.IndexOf(w) != 6);
            var restOn = rest.Any(w => w.State);
            Assert.IsFalse(restOn);
            
            stepperOutput = stepper.Step(new Bit(true), new Bit(true));
            
            Assert.IsTrue(stepperOutput[0].State);
            rest = stepperOutput.Where(w => stepperOutput.IndexOf(w) != 0);
            restOn = rest.Any(w => w.State);
            Assert.IsFalse(restOn);
        }

        [Test]
        public void InfiniteStepper()
        {
            var stepper = TestUtils.CreateStepper();

            var stepperOutput = new StepperOutput(TestUtils.CreateBitFactory());

            for (var i = 0; i < 8; i++)
            {
                stepper.Step(new Bit(true), new Bit(false));
                stepperOutput = stepper.Step(new Bit(false), new Bit(false));
            }

            Assert.IsTrue(stepperOutput[6].State);
            var output = stepperOutput;
            var rest = stepperOutput.Where(w => output.IndexOf(w) != 6);
            var restOn = rest.Any(w => w.State);
            Assert.IsFalse(restOn);
            
            stepperOutput = stepper.Step(new Bit(true));
            
            Assert.IsTrue(stepperOutput[0].State);
            rest = stepperOutput.Where(w => stepperOutput.IndexOf(w) != 0);
            restOn = rest.Any(w => w.State);
            Assert.IsFalse(restOn);
        }
    }
}