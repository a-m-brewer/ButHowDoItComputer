using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    public class Base10ConverterTests
    {
        private Base10Converter _sut;
        private uint _twenty;

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
                true
            };

            _twenty = 20;

            _sut = new Base10Converter();
        }

        [Test]
        public void CanConvertBitsToBase10()
        {
            Assert.AreEqual(_twenty, _sut.ToInt(_twentyInBits));
        }

        [Test]
        public void CanConvertIntToBits()
        {
            var result = _sut.ToBit(_twenty).ToList();
            for (var i = 0; i < _twentyInBits.Count; i++) Assert.AreEqual(_twentyInBits[i], result[i]);
        }
    }
}