using System.Collections.Generic;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    public class XOrTests
    {
        private static readonly object[] OrData = {
            new object[] {false, new bool[] {false, false}},
            new object[] {true, new bool[] {false, true}},
            new object[] {true, new bool[] {true, false}},
            new object[] {false, new bool[] {true, true}},
            
            new object[] {false, new bool[] {false, false, false}},
            new object[] {true, new bool[] {false, false, true}},
            new object[] {true, new bool[] {false, true, false}},
            new object[] {false, new bool[] {false, true, true}},
            new object[] {true, new bool[] {true, false, false}},
            new object[] {false, new bool[] {true, false, true}},
            new object[] {false, new bool[] {true, true, false}},
            new object[] {true, new bool[] {true, true, true}},
        };


        [Test, TestCaseSource(nameof(OrData))]
        public void ReturnsCorrectNewBit(bool expected, bool[] bits)
        {
            var sut = new XOr(new Not(), new NAnd(new Not(), new And()));
            Assert.AreEqual(expected, sut.Apply(bits));
        }

    }
}