using System.Collections.Generic;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
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
            var sut = new NAnd(new Not(new BitFactory()), new And(new BitFactory()));
            Assert.AreEqual(expected, sut.Apply(bits).State);
        }
    }
}