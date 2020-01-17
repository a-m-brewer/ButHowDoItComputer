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
    public class ByteOrTests
    {
        [Test]
        public void ByteAndRunsAsExpected()
        {
            var byteFactory = new ByteFactory(new BitFactory(), new Base10Converter(new BitFactory()));
            var sut = new ByteOr(
                new Or(new Not(new BitFactory()), new NAnd(new Not(new BitFactory()), new And(new BitFactory()))),
                byteFactory);

            var result = sut.Apply(new [] {byteFactory.Create(0), byteFactory.Create(255)});

            Assert.IsTrue(result.All(a => a.State));
        }
    }
}