using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    public class ByteXOrTests
    {
        [Test]
        public void ByteAndRunsAsExpected()
        {
            var byteFactory = new ByteFactory(new BitFactory(), new Base10Converter(new BitFactory()));
            var sut = new ByteXOr(
                new XOr(new Not(new BitFactory()), new NAnd(new Not(new BitFactory()), new And(new BitFactory()))),
                byteFactory);

            var result = sut.Apply(new List<IByte> {byteFactory.Create(255), byteFactory.Create(255)});

            Assert.IsFalse(result.Any(a => a.State));
        }
    }
}