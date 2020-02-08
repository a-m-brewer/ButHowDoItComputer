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
            var byteFactory = new ByteFactory(new Base10Converter());
            var inverter = new Inverter(new Not(), byteFactory);
            var sut = new Components.RegisterInverter(inverter);
            var input = byteFactory.Create(255);
            var expectedOutput = byteFactory.Create(0);

            var inputRegister = TestUtils.CreateRegister();
            var outputRegister = TestUtils.CreateRegister();
            inputRegister.Input = input;
            sut.Apply(inputRegister, outputRegister);

            for (var i = 0; i < expectedOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i], outputRegister.Output[i]);
            }
        }
    }
}