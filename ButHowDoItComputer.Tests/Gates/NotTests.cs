using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Gates
{
    [TestFixture]
    public class NotTests
    {
        [Test]
        [TestCase(true, false)]
        [TestCase(false, true)]
        public void CanNegateAState(bool before, bool after)
        {
            Assert.AreEqual(after, new Not().Apply(before));
        }
    }
}