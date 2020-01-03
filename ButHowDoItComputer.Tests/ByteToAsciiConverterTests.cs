using System;
using System.Linq;
using System.Text;
using ButHowDoItComputer.Codes.ASCII;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class ByteToAsciiConverterTests
    {
        private static readonly object[] TestData = Enumerable
            .Range(0, 127)
            .Select(s =>
                new object[]
                {
                    new ByteToBase10Converter(new BitFactory(), new ByteFactory(new BitFactory()),
                        new Base10Converter(new BitFactory())).ToByte((uint) s),
                    Convert.ToChar((char) s).ToString()
                })
            .ToArray();

        [Test, TestCaseSource(nameof(TestData))]
        public void CanConvertFromByteToAscii(IByte inputByte, string expectedChar)
        {
            var result = new ByteToAsciiConverter(new ByteToBase10Converter(new BitFactory(),
                new ByteFactory(new BitFactory()), new Base10Converter(new BitFactory()))).ToAscii(inputByte);
            Assert.AreEqual(expectedChar, result);
        }
        
        [Test, TestCaseSource(nameof(TestData))]
        public void CanConvertFromAsciiToByte(IByte outputByte, string inputChar)
        {
            var result =
                new ByteToAsciiConverter(new ByteToBase10Converter(new BitFactory(), new ByteFactory(new BitFactory()),
                        new Base10Converter(new BitFactory())))
                    .ToByte(inputChar);

            for (var i = 0; i < outputByte.Count; i++)
            {
                Assert.AreEqual(outputByte[i].State, result[i].State);
            }
        }
    }
}