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
            new object[] {false, new bool[] {true, true}},
            new object[] {true, new bool[] {true, false}},
            new object[] {true, new bool[] {false, true}},
            new object[] {true, new bool[] {false, false}}
        };


        [Test, TestCaseSource(nameof(NAndData))]
        public void ReturnsCorrectNewBit(bool expected, bool[] bits)
        {
            var sut = new NAnd(new Not(), new And());
            Assert.AreEqual(expected, sut.Apply(bits));
        }
    }
}