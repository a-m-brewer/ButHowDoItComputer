using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    public class XOrTests
    {
        private static readonly object[] OrData =
        {
            new object[] {false, new[] {false, false}},
            new object[] {true, new[] {false, true}},
            new object[] {true, new[] {true, false}},
            new object[] {false, new[] {true, true}},

            new object[] {false, new[] {false, false, false}},
            new object[] {true, new[] {false, false, true}},
            new object[] {true, new[] {false, true, false}},
            new object[] {false, new[] {false, true, true}},
            new object[] {true, new[] {true, false, false}},
            new object[] {false, new[] {true, false, true}},
            new object[] {false, new[] {true, true, false}},
            new object[] {true, new[] {true, true, true}}
        };


        [Test]
        [TestCaseSource(nameof(OrData))]
        public void ReturnsCorrectNewBit(bool expected, bool[] bits)
        {
            var sut = new XOr(new Not(), new NAnd(new Not(), new And()));
            Assert.AreEqual(expected, sut.Apply(bits));
        }
    }
}