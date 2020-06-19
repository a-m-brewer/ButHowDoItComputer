using System;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Codes.ASCII;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Codes.Ascii
{
    [TestFixture]
    public class ByteToAsciiConverterTests
    {
        private static readonly object[] TestData = Enumerable
            .Range(0, 127)
            .Select(s =>
                new object[]
                {
                    new ByteToBase10Converter(new ByteFactory(new Base10Converter()),
                        new Base10Converter()).ToByte((uint) s),
                    Convert.ToChar((char) s).ToString()
                })
            .ToArray();

        [Test]
        [TestCaseSource(nameof(TestData))]
        public void CanConvertFromAsciiToByte(IList<bool> outputByte, string inputChar)
        {
            var result =
                new ByteToAsciiConverter(new ByteToBase10Converter(new ByteFactory(new Base10Converter()),
                        new Base10Converter()))
                    .ToByte(inputChar);

            for (var i = 0; i < outputByte.Count; i++) Assert.AreEqual(outputByte[i], result[i]);
        }

        [Test]
        [TestCaseSource(nameof(TestData))]
        public void CanConvertFromByteToAscii(IList<bool> inputByte, string expectedChar)
        {
            var result = new ByteToAsciiConverter(new ByteToBase10Converter(
                new ByteFactory(new Base10Converter()), new Base10Converter())).ToAscii(inputByte);
            Assert.AreEqual(expectedChar, result);
        }
    }
}