using System.Collections.Generic;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    public class OrTests
    {
        private static readonly object[] OrData = {
            new object[] {false, new IBit[] {new Bit(false), new Bit(false)}},
            new object[] {true, new IBit[] {new Bit(false), new Bit(true)}},
            new object[] {true, new IBit[] {new Bit(true), new Bit(false)}},
            new object[] {true, new IBit[] {new Bit(true), new Bit(true)}}
        };


        [Test, TestCaseSource(nameof(OrData))]
        public void ReturnsCorrectNewBit(bool expected, IBit[] bits)
        {
            var sut = new Or(new Not(new BitFactory()), new NAnd(new Not(new BitFactory()), new And(new BitFactory())));
            Assert.AreEqual(expected, sut.Apply(bits).State);
        }
    }
}