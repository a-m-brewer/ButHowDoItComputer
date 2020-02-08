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
            new object[] {false, new bool[] {false, false}},
            new object[] {true, new bool[] {false, true}},
            new object[] {true, new bool[] {true, false}},
            new object[] {true, new bool[] {true, true}}
        };


        [Test, TestCaseSource(nameof(OrData))]
        public void ReturnsCorrectNewBit(bool expected, bool[] bits)
        {
            var sut = new Or(new Not(), new NAnd(new Not(), new And()));
            Assert.AreEqual(expected, sut.Apply(bits));
        }
    }
}