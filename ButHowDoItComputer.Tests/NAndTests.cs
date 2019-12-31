using System.Collections.Generic;
using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class NAndTests
    {
        private static readonly object[] NAndData = {
            new object[] {false, new List<Bit> {new Bit(true), new Bit(true)}},
            new object[] {true, new List<Bit> {new Bit(true), new Bit(false)}},
            new object[] {true, new List<Bit> {new Bit(false), new Bit(false)}}
        };


        [Test, TestCaseSource(nameof(NAndData))]
        public void ReturnsCorrectNewBit(bool expected, List<Bit> bits)
        {
            var sut = new NAnd();
            Assert.AreEqual(expected, sut.Apply(bits).State);
        }
    }
}