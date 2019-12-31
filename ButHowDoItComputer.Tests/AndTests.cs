using System;
using System.Collections.Generic;
using ButHowDoItComputer.Gates;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class AndTests
    {
        private static readonly object[] _andData = new[]
        {
            new object[] {true, new List<Bit> {new Bit(true), new Bit(true)}},
            new object[] {false, new List<Bit> {new Bit(true), new Bit(false)}},
            new object[] {false, new List<Bit> {new Bit(false), new Bit(false)}}
        };


        [Test, TestCaseSource(nameof(_andData))]
        public void ReturnsCorrectNewBit(bool expected, List<Bit> bits)
        {
            var sut = new And();
            Assert.AreEqual(expected, sut.Apply(bits).State);
        }
    }
}