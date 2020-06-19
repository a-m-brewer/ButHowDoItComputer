using System;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Parts
{
    [TestFixture]
    public class RegisterTests
    {
        [SetUp]
        public void Setup()
        {
            _byteFactory = new ByteFactory(new Base10Converter());
            _memoryGateFactory = new MemoryGateFactory(new NAnd(new Not()));
            _busDataTypeMemoryGate = new BusDataTypeMemoryGate<IList<bool>>(new NAnd(new Not()), _byteFactory, 8);
            _busDataTypeEnabler = new BusDataTypeEnabler<IList<bool>>(_byteFactory);
            _sut = new BusDataTypeRegister<IList<bool>>(_busDataTypeMemoryGate, _busDataTypeEnabler, _byteFactory, wire => {});
        }

        private ByteFactory _byteFactory;
        private MemoryGateFactory _memoryGateFactory;
        private BusDataTypeMemoryGate<IList<bool>> _busDataTypeMemoryGate;
        private BusDataTypeEnabler<IList<bool>> _busDataTypeEnabler;
        private BusDataTypeRegister<IList<bool>> _sut;

        [Test]
        [TestCase(false, false, false)]
        [TestCase(false, false, true)]
        [TestCase(false, true, false)]
        [TestCase(false, true, true)]
        [TestCase(true, false, false)]
        [TestCase(true, false, true)]
        [TestCase(true, true, false)]
        [TestCase(true, true, true)]
        public void TestGateWorksAsIntended(bool input, bool set, bool enable)
        {
            var expected = input && set && enable;
            var bits = Enumerable.Range(0, 8).Select(s => input).ToArray();
            _sut.Set = set;
            _sut.Enable = enable;
            var result = _sut.Apply(bits);
            Assert.AreEqual(expected, result.All(a => a));
        }
    }
}