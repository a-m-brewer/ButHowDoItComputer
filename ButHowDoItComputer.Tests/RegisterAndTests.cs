using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Components;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class RegisterAndTests
    {
        [Test]
        public void RegisterAndRunsAsExpected()
        {
            var byteFactory = new ByteFactory(new BitFactory(), new Base10Converter(new BitFactory()));
            var sut = new RegisterAnd(new ByteAnd(new And(new BitFactory()),
                new ByteFactory(new BitFactory(), new Base10Converter(new BitFactory()))));
            
            var on = byteFactory.Create(255);
            var off = byteFactory.Create(0);

            var inputRegister1 = TestUtils.CreateRegister();
            var inputRegister2 = TestUtils.CreateRegister();
            var outputRegister = TestUtils.CreateRegister();
            inputRegister1.Input = on;
            inputRegister2.Input = off;
            sut.Apply(new List<IRegister<IByte>> {inputRegister1, inputRegister2}, outputRegister);

            Assert.IsFalse(outputRegister.Output.Any(a => a.State));
        }
    }
}