using System.Collections.Generic;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class AndTests
    {
        private static readonly object[] AndData = {
            new object[] {true, new List<Bit> {new Bit(true), new Bit(true)}},
            new object[] {false, new List<Bit> {new Bit(true), new Bit(false)}},
            new object[] {false, new List<Bit> {new Bit(false), new Bit(false)}}
        };


        [Test, TestCaseSource(nameof(AndData))]
        public void ReturnsCorrectNewBit(bool expected, List<Bit> bits)
        {
            var sut = new And(new BitFactory());
            Assert.AreEqual(expected, sut.Apply(bits).State);
        }
    }
}