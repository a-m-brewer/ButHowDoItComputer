using System.Linq;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Gates
{
    [TestFixture]
    public class ByteAndTests
    {
        [Test]
        public void ByteAndRunsAsExpected()
        {
            var byteFactory = new ByteFactory(new Base10Converter());
            var sut = new BusDataTypeAnd<IByte>(new ByteFactory(new Base10Converter()));

            var result = sut.Apply(byteFactory.Create(0), byteFactory.Create(255));

            Assert.IsFalse(result.Any(a => a));
        }
    }
}