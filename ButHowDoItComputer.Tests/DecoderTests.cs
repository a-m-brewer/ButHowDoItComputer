using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class DecoderTests
    {
        private Decoder _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Decoder(new Not(), new And());
        }

        [Test]
        [TestCase(false, false, true, false, false, false)]
        [TestCase(false, true, false, true, false, false)]
        [TestCase(true, false, false, false, true, false)]
        [TestCase(true, true, false, false, false, true)]
        public void TruthTableTest(bool a, bool b, bool o1, bool o2, bool o3, bool o4)
        {
            var result = _sut.Apply(a, b).ToList();
            var expected = new bool[] {o1, o2, o3, o4};

            Assert.AreEqual(4, result.Count); 
            
            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i]); 
            }
        }

        [Test]
        [TestCase(false, false, false, true, false, false, false, false, false, false, false)]
        [TestCase(false, false, true, false, true, false, false, false, false, false, false)]
        [TestCase(false, true, false, false, false, true, false, false, false, false, false)]
        [TestCase(false, true, true, false, false, false, true, false, false, false, false)]
        [TestCase(true, false, false, false, false, false, false, true, false, false, false)]
        [TestCase(true, false, true, false, false, false, false, false, true, false, false)]
        [TestCase(true, true, false, false, false, false, false, false, false, true, false)]
        [TestCase(true, true, true, false, false, false, false, false, false, false, true)]
        public void CanBeExtended(bool a, bool b, bool c, bool o1, bool o2, bool o3, bool o4, bool o5, bool o6, bool o7, bool o8)
        {
            var result = _sut.Apply(new [] {a, b, c}).ToList();
            var expected = new bool[]
            {
                o1, o2, o3, o4, o5, o6, o7, o8
            };
            
            Assert.AreEqual(expected.Length, result.Count);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i]); 
            }
        }
    }
}