using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Parts
{
    [TestFixture]
    public class ClockTests
    {
        [Test]
        public void ClockStartsAsAllFalse()
        {
            var clock = TestUtils.CreateClock();

            Assert.IsFalse(clock.Clk);
            Assert.IsFalse(clock.ClkD);
            Assert.IsFalse(clock.ClkE);
            Assert.IsFalse(clock.ClkS);
        }

        [Test]
        public void FirstCycleIsCorrect()
        {
            var clock = TestUtils.CreateClock();
            var cycle1 = clock.Cycle();

            Assert.IsTrue(clock.Clk);
            Assert.IsFalse(clock.ClkD);
            Assert.IsTrue(clock.ClkE);
            Assert.IsFalse(clock.ClkS);

            Assert.IsTrue(cycle1.Clk);
            Assert.IsFalse(cycle1.ClkD);
            Assert.IsTrue(cycle1.ClkE);
            Assert.IsFalse(cycle1.ClkS);
        }

        [Test]
        public void ForthCycleIsCorrect()
        {
            var clock = TestUtils.CreateClock();
            clock.Cycle();
            clock.Cycle();
            clock.Cycle();
            var cycle4 = clock.Cycle();

            Assert.IsFalse(clock.Clk);
            Assert.IsFalse(clock.ClkD);
            Assert.IsFalse(clock.ClkE);
            Assert.IsFalse(clock.ClkS);

            Assert.IsFalse(cycle4.Clk);
            Assert.IsFalse(cycle4.ClkD);
            Assert.IsFalse(cycle4.ClkE);
            Assert.IsFalse(cycle4.ClkS);
        }

        [Test]
        public void SecondCycleIsCorrect()
        {
            var clock = TestUtils.CreateClock();
            clock.Cycle();
            var cycle2 = clock.Cycle();

            Assert.IsTrue(clock.Clk);
            Assert.IsTrue(clock.ClkD);
            Assert.IsTrue(clock.ClkE);
            Assert.IsTrue(clock.ClkS);

            Assert.IsTrue(cycle2.Clk);
            Assert.IsTrue(cycle2.ClkD);
            Assert.IsTrue(cycle2.ClkE);
            Assert.IsTrue(cycle2.ClkS);
        }

        [Test]
        public void ThirdCycleIsCorrect()
        {
            var clock = TestUtils.CreateClock();
            clock.Cycle();
            clock.Cycle();
            var cycle3 = clock.Cycle();

            Assert.IsFalse(clock.Clk);
            Assert.IsTrue(clock.ClkD);
            Assert.IsTrue(clock.ClkE);
            Assert.IsFalse(clock.ClkS);

            Assert.IsFalse(cycle3.Clk);
            Assert.IsTrue(cycle3.ClkD);
            Assert.IsTrue(cycle3.ClkE);
            Assert.IsFalse(cycle3.ClkS);
        }
    }
}