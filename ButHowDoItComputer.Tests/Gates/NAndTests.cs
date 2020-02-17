using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Gates
{
    [TestFixture]
    public class NAndTests
    {
        private static readonly object[] NAndData =
        {
            new object[] {false, new[] {true, true}},
            new object[] {true, new[] {true, false}},
            new object[] {true, new[] {false, true}},
            new object[] {true, new[] {false, false}}
        };


        [Test]
        [TestCaseSource(nameof(NAndData))]
        public void ReturnsCorrectNewBit(bool expected, bool[] bits)
        {
            var sut = new NAnd(new Not(), new And());
            Assert.AreEqual(expected, sut.Apply(bits));
        }
    }
}