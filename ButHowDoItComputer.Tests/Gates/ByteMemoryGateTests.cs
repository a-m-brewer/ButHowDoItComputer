using System;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Gates
{
    [TestFixture]
    public class ByteMemoryGateTests
    {
        [Test]
        public void CanKeepTheValueOfAByte()
        {
            var sut = new BusDataTypeMemoryGate<IList<bool>>(new NAnd(new Not()),
                new ByteFactory(new Base10Converter()), 8);

            var input = Enumerable.Range(0, 8).Select(s => true).ToArray();

            var result = sut.Apply(input, true);

            for (var i = 0; i < input.Length; i++) Assert.AreEqual(input[i], result[i]);

            input = Enumerable.Range(0, 8).Select(s => false).ToArray();

            result = sut.Apply(input, false);

            for (var i = 0; i < input.Length; i++) Assert.AreEqual(true, result[i]);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void CanStoreAByte(bool expected)
        {
            var sut = new BusDataTypeMemoryGate<IList<bool>>(new NAnd(new Not()),
                new ByteFactory(new Base10Converter()), 8);

            var input = Enumerable.Range(0, 8).Select(s => expected).ToArray();

            var result = sut.Apply(input, true);

            for (var i = 0; i < input.Length; i++) Assert.AreEqual(input[i], result[i]);
        }
    }
}