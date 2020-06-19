using System.Linq;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Gates
{
    public class ByteXOrTests
    {
        [Test]
        public void ByteAndRunsAsExpected()
        {
            var byteFactory = new ByteFactory(new Base10Converter());
            var sut = new BusDataTypeXOr<IByte>(
                new XOr(new Not(), new NAnd(new Not())),
                byteFactory);

            var result = sut.Apply(byteFactory.Create(255), byteFactory.Create(255));

            Assert.IsFalse(result.Any(a => a));
        }
    }
}