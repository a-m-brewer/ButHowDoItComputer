using ButHowDoItComputer.Components;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    public class RegisterInverterTests
    {
        [Test]
        public void InvertsAllInputs()
        {
            var byteFactory = new ByteFactory(new BitFactory(), new Base10Converter(new BitFactory()));
            var inverter = new Inverter(new Not(new BitFactory()), byteFactory);
            var sut = new Components.RegisterInverterTests(inverter);
            var input = byteFactory.Create(255);
            var expectedOutput = byteFactory.Create(0);

            var inputRegister = TestUtils.CreateRegister();
            var outputRegister = TestUtils.CreateRegister();
            inputRegister.Input = input;
            sut.Apply(inputRegister, outputRegister);

            for (var i = 0; i < expectedOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i].State, outputRegister.Output[i].State);
            }
        }
    }
}