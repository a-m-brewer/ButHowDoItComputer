using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Gates
{
    public class OrTests
    {
        private static readonly object[] OrData =
        {
            new object[] {false, new[] {false, false}},
            new object[] {true, new[] {false, true}},
            new object[] {true, new[] {true, false}},
            new object[] {true, new[] {true, true}}
        };


        [Test]
        [TestCaseSource(nameof(OrData))]
        public void ReturnsCorrectNewBit(bool expected, bool[] bits)
        {
            var sut = new Or(new Not(), new NAnd(new Not()));
            Assert.AreEqual(expected, sut.ApplyParams(bits));
        }
    }
}