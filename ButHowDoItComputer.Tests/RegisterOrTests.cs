using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Components;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class RegisterOrTests
    {
        [Test]
        public void RegisterOrRunsAsExpected()
        {
            var byteFactory = new ByteFactory(new BitFactory(), new Base10Converter(new BitFactory()));
            var sut = new RegisterOr(new ByteOr(
                new Or(new Not(new BitFactory()), new NAnd(new Not(new BitFactory()), new And(new BitFactory()))),
                byteFactory));

            var on = byteFactory.Create(255);
            var off = byteFactory.Create(0);

            var inputRegister1 = TestUtils.CreateRegister();
            var inputRegister2 = TestUtils.CreateRegister();
            var outputRegister = TestUtils.CreateRegister();
            inputRegister1.Input = on;
            inputRegister2.Input = off;
            sut.Apply(new List<IRegister> {inputRegister1, inputRegister2}, outputRegister);

            Assert.IsTrue(outputRegister.Output.All(a => a.State));
        }
    }
}