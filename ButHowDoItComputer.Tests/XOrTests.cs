using System.Collections.Generic;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    public class XOrTests
    {
        private static readonly object[] OrData = {
            new object[] {false, new List<Bit> {new Bit(false), new Bit(false)}},
            new object[] {true, new List<Bit> {new Bit(false), new Bit(true)}},
            new object[] {true, new List<Bit> {new Bit(true), new Bit(false)}},
            new object[] {false, new List<Bit> {new Bit(true), new Bit(true)}},
            
            new object[] {false, new List<Bit> {new Bit(false), new Bit(false), new Bit(false)}},
            new object[] {true, new List<Bit> {new Bit(false), new Bit(false), new Bit(true)}},
            new object[] {true, new List<Bit> {new Bit(false), new Bit(true), new Bit(false)}},
            new object[] {false, new List<Bit> {new Bit(false), new Bit(true), new Bit(true)}},
            new object[] {true, new List<Bit> {new Bit(true), new Bit(false), new Bit(false)}},
            new object[] {false, new List<Bit> {new Bit(true), new Bit(false), new Bit(true)}},
            new object[] {false, new List<Bit> {new Bit(true), new Bit(true), new Bit(false)}},
            new object[] {true, new List<Bit> {new Bit(true), new Bit(true), new Bit(true)}},
        };


        [Test, TestCaseSource(nameof(OrData))]
        public void ReturnsCorrectNewBit(bool expected, List<Bit> bits)
        {
            var sut = new XOr(new Not(new BitFactory()), new NAnd(new Not(new BitFactory()), new And(new BitFactory())));
            Assert.AreEqual(expected, sut.Apply(bits).State);
        }

    }
}