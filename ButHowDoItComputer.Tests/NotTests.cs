using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class NotTests
    {
        [Test]
        [TestCase(true, false)]
        [TestCase(false, true)]
        public void CanNegateAState(bool before, bool after)
        {
            Assert.AreEqual(after, new Not(new BitFactory()).Apply(new Bit(before)).State);
        }
    }
}