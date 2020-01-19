using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class ClockTests
    {
        [Test]
        public void ClockStartsAsAllFalse()
        {
            var clock = TestUtils.CreateClock();
            
            Assert.IsFalse(clock.Clk.State);
            Assert.IsFalse(clock.ClkD.State);
            Assert.IsFalse(clock.ClkE.State);
            Assert.IsFalse(clock.ClkS.State);
        }

        [Test]
        public void FirstCycleIsCorrect()
        {
            var clock = TestUtils.CreateClock();
            var cycle1 = clock.Cycle();
            
            Assert.IsTrue(clock.Clk.State);
            Assert.IsFalse(clock.ClkD.State);
            Assert.IsTrue(clock.ClkE.State);
            Assert.IsFalse(clock.ClkS.State);
            
            Assert.IsTrue(cycle1.Clk.State);
            Assert.IsFalse(cycle1.ClkD.State);
            Assert.IsTrue(cycle1.ClkE.State);
            Assert.IsFalse(cycle1.ClkS.State);
        }

        [Test]
        public void SecondCycleIsCorrect()
        {
            var clock = TestUtils.CreateClock();
            clock.Cycle();
            var cycle2 = clock.Cycle();
            
            Assert.IsTrue(clock.Clk.State);
            Assert.IsTrue(clock.ClkD.State);
            Assert.IsTrue(clock.ClkE.State);
            Assert.IsTrue(clock.ClkS.State);
            
            Assert.IsTrue(cycle2.Clk.State);
            Assert.IsTrue(cycle2.ClkD.State);
            Assert.IsTrue(cycle2.ClkE.State);
            Assert.IsTrue(cycle2.ClkS.State);
        }
        
        [Test]
        public void ThirdCycleIsCorrect()
        {
            var clock = TestUtils.CreateClock();
            clock.Cycle();
            clock.Cycle();
            var cycle3 = clock.Cycle();
            
            Assert.IsFalse(clock.Clk.State);
            Assert.IsTrue(clock.ClkD.State);
            Assert.IsTrue(clock.ClkE.State);
            Assert.IsFalse(clock.ClkS.State);
            
            Assert.IsFalse(cycle3.Clk.State);
            Assert.IsTrue(cycle3.ClkD.State);
            Assert.IsTrue(cycle3.ClkE.State);
            Assert.IsFalse(cycle3.ClkS.State);
        }
        
        [Test]
        public void ForthCycleIsCorrect()
        {
            var clock = TestUtils.CreateClock();
            clock.Cycle();
            clock.Cycle();
            clock.Cycle();
            var cycle4 = clock.Cycle();
            
            Assert.IsFalse(clock.Clk.State);
            Assert.IsFalse(clock.ClkD.State);
            Assert.IsFalse(clock.ClkE.State);
            Assert.IsFalse(clock.ClkS.State);
            
            Assert.IsFalse(cycle4.Clk.State);
            Assert.IsFalse(cycle4.ClkD.State);
            Assert.IsFalse(cycle4.ClkE.State);
            Assert.IsFalse(cycle4.ClkS.State);
        }
    }
}