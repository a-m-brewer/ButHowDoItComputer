using System;
using System.Collections.Generic;
using ButHowDoItComputer.Codes.ASCII;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils;
using NUnit.Framework;
using Byte = ButHowDoItComputer.DataTypes.Byte;

namespace ButHowDoItComputer.Tests
{
    public class ByteToBase10ConverterTests
    {
        private List<IBit> _twentyInBits;
        private uint _twenty;
        private ByteToBase10Converter _sut;
        private Byte _twentyByte;

        [SetUp]
        public void Setup()
        {
            _twentyInBits = new List<IBit>
            {
                new Bit(false),
                new Bit(false),
                new Bit(true),
                new Bit(false),
                new Bit(true),
                new Bit(false),
                new Bit(false),
                new Bit(false)
            };
            
            _twenty = 20;
            _twentyByte = new Byte(_twentyInBits.ToArray(), new BitFactory());

            _sut = new ByteToBase10Converter(new BitFactory(), new ByteFactory(new BitFactory(), new Base10Converter(new BitFactory())),
                new Base10Converter(new BitFactory()));
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
            for (var i = 0; i < _twentyInBits.Count; i++)
            {
                Assert.AreEqual(_twentyInBits[i].State, result[i].State);
            }
        }

        [Test]
        public void ThrowsOutOfMemoryExceptionIfNumberIsGreaterThanBytesCapacity()
        {
            const int large = 256;
            Assert.Throws<OutOfMemoryException>(() => { _sut.ToByte(large); });
        }
    }
}