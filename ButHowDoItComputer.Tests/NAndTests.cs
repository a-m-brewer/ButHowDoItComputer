using System.Collections.Generic;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class NAndTests
    {
        private static readonly object[] NAndData = {
            new object[] {false, new IBit[] {new Bit(true), new Bit(true)}},
            new object[] {true, new IBit[] {new Bit(true), new Bit(false)}},
            new object[] {true, new IBit[] {new Bit(false), new Bit(true)}},
            new object[] {true, new IBit[] {new Bit(false), new Bit(false)}}
        };


        [Test, TestCaseSource(nameof(NAndData))]
        public void ReturnsCorrectNewBit(bool expected, IBit[] bits)
        {
            var sut = new NAnd(new Not(new BitFactory()), new And(new BitFactory()));
            Assert.AreEqual(expected, sut.Apply(bits).State);
        }
    }
}