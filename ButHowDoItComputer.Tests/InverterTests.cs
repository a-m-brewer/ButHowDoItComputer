using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class InverterTests
    {
        [Test]
        public void InvertsAllInputs()
        {
            var byteFactory = new ByteFactory(new BitFactory(), new Base10Converter(new BitFactory()));
            var sut = new Inverter(new Not(new BitFactory()), byteFactory);
            var input = byteFactory.Create(255);
            var expectedOutput = byteFactory.Create(0);
            
            var result = sut.Invert(input);

            for (var i = 0; i < expectedOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i].State, result[i].State);
            }
        }
    }
}