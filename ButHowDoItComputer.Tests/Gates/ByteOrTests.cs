using System.Linq;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests.Gates
{
    [TestFixture]
    public class ByteOrTests
    {
        [Test]
        public void ByteAndRunsAsExpected()
        {
            var byteFactory = new ByteFactory(new Base10Converter());
            var sut = new BusDataTypeOr<IByte>(
                new Or(new Not(), new NAnd(new Not(), new And())),
                byteFactory);

            var result = sut.Apply(byteFactory.Create(0), byteFactory.Create(255));

            Assert.IsTrue(result.All(a => a));
        }
    }
}