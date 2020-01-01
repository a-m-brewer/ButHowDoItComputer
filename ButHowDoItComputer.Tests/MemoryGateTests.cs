using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class MemoryGateTests
    {
        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void CanStoreABit(bool expected)
        {
            var sut = new MemoryGate(new NAnd(new Not(new BitFactory()), new And(new BitFactory())), new BitFactory());
            
            var input = new Bit(expected);
            var set = new Bit(true);

            var result = sut.Apply(input, set);
            
            Assert.AreEqual(input.State, result.State);
        }

        [Test]
        public void CanKeepTheValueOfABit()
        {
            var sut = new MemoryGate(new NAnd(new Not(new BitFactory()), new And(new BitFactory())), new BitFactory());
            
            // set the initial state of the memory to on
            var input = new Bit(true);
            var set = new Bit(true);
            var result = sut.Apply(input, set);
            
            // assert that the bit is set
            Assert.AreEqual(input.State, result.State);
            
            // turn off set mode and try and set the input bit to off
            input = new Bit(false);
            set = new Bit(false);

            result = sut.Apply(input, set);
            Assert.AreEqual(true, result.State);
        }
        
        // this test might be a bit pointless but it gives me piece of mind
        [Test]
        public void CanSetTheBitAgain()
        {
            var sut = new MemoryGate(new NAnd(new Not(new BitFactory()), new And(new BitFactory())), new BitFactory());
            
            // set the initial state of the memory to on
            var input = new Bit(true);
            var set = new Bit(true);
            var result = sut.Apply(input, set);
            
            // assert that the bit is set
            Assert.AreEqual(input.State, result.State);
            
            // turn off set mode and try and set the input bit to off
            input = new Bit(false);
            set = new Bit(false);
            result = sut.Apply(input, set);
            
            Assert.AreEqual(true, result.State);
            
            input = new Bit(false);
            set = new Bit(true);
            result = sut.Apply(input, set);
            
            Assert.AreEqual(false, result.State);
        }
    }
}