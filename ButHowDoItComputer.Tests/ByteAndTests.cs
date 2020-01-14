using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class ByteAndTests
    {
        [Test]
        public void ByteAndRunsAsExpected()
        {
            var byteFactory = new ByteFactory(new BitFactory(), new Base10Converter(new BitFactory()));
            var sut = new ByteAnd(new And(new BitFactory()), new ByteFactory(new BitFactory(), new Base10Converter(new BitFactory())));

            var result = sut.Apply(new List<IByte> {byteFactory.Create(0), byteFactory.Create(255)});

            Assert.IsFalse(result.Any(a => a.State));
        }
    }
}