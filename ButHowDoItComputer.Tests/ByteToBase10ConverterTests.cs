using System;
using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Utils;
using NUnit.Framework;
using Byte = ButHowDoItComputer.DataTypes.Byte;

namespace ButHowDoItComputer.Tests
{
    public class ByteToBase10ConverterTests
    {
        private ByteToBase10Converter _sut;
        private uint _twenty;
        private Byte _twentyByte;
        private List<bool> _twentyInBits;

        [SetUp]
        public void Setup()
        {
            _twentyInBits = new List<bool>
            {
                false,
                false,
                true,
                false,
                true,
                false,
                false,
                false
            };

            _twenty = 20;
            _twentyByte = new Byte(_twentyInBits.ToArray());

            _sut = new ByteToBase10Converter(new ByteFactory(new Base10Converter()),
                new Base10Converter());
        }

        [Test]
        public void CanConvertByteToBase10()
        {
            Assert.AreEqual(_twenty, _sut.ToInt(_twentyByte));
        }

        [Test]
        public void CanConvertIntToByte()
        {
            var result = _sut.ToByte(_twenty);
            for (var i = 0; i < _twentyInBits.Count; i++) Assert.AreEqual(_twentyInBits[i], result[i]);
        }

        [Test]
        public void ThrowsOutOfMemoryExceptionIfNumberIsGreaterThanBytesCapacity()
        {
            const int large = 256;
            Assert.Throws<OutOfMemoryException>(() => { _sut.ToByte(large); });
        }
    }
}