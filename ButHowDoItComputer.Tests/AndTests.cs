using System.Collections.Generic;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class AndTests
    {
        private static readonly object[] AndData = {
            new object[] {true, new[] {new Bit(true), new Bit(true)}},
            new object[] {false, new[] {new Bit(true), new Bit(false)}},
            new object[] {false, new[] {new Bit(false), new Bit(false)}}
        };


        [Test, TestCaseSource(nameof(AndData))]
        public void ReturnsCorrectNewBit(bool expected, IBit[] bits)
        {
            var sut = new And(new BitFactory());
            Assert.AreEqual(expected, sut.Apply(bits).State);
        }
    }
}