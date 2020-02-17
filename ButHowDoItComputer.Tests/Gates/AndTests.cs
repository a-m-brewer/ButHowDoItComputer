using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Gates
{
    [TestFixture]
    public class AndTests
    {
        private static readonly object[] AndData =
        {
            new object[] {true, new[] {true, true}},
            new object[] {false, new[] {true, false}},
            new object[] {false, new[] {false, false}}
        };


        [Test]
        [TestCaseSource(nameof(AndData))]
        public void ReturnsCorrectNewBit(bool expected, bool[] bits)
        {
            var sut = new And();
            Assert.AreEqual(expected, sut.Apply(bits));
        }
    }
}