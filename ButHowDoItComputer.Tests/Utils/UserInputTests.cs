using System.Collections.Generic;
using System.Linq;
using System.Text;
using ButHowDoItComputer.Codes.ASCII;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Utils;
using ButHowDoItComputer.Utils.Interfaces;
using Moq;
using NUnit.Framework;


namespace ButHowDoItComputer.Tests.Utils
{
    [TestFixture]
    public class UserInputTests
    {
        private Mock<IInputDevice> _inputDevice;
        private UserInput _sut;

        [SetUp]
        public void SetUp()
        {
            _inputDevice = new Mock<IInputDevice>();
            _sut = new UserInput(_inputDevice.Object,
                new ByteToAsciiConverter(new ByteToBase10Converter(new ByteFactory(new Base10Converter()),
                    new Base10Converter())));
        }

        [Test]
        public void CanConvertStringToIBytes()
        {
            const string fullString = "abcdefghijklmnopqrstuvwxyz";
            
            var expectedArray = fullString.Select(ToByte).ToList();

            _inputDevice.Setup(s => s.Get()).Returns(fullString);

            var result = _sut.Input();

            for (var i = 0; i < expectedArray.Count; i++)
            {
                for (var j = 0; j < expectedArray[i].Count; j++)
                {
                    Assert.AreEqual(expectedArray[i][j], result[i][j]);
                }
            }
        }

        private IList<bool> ToByte(char c)
        {
            var asByte = Encoding.ASCII.GetBytes(new[] {c}).First();
            var bits = GetBits(asByte);
            return bits.ToArray();
        }
        
        private IEnumerable<bool> GetBits(byte b)
        {
            for(var i = 0; i < 8; i++)
            {
                yield return (b % 2 != 0);
                b = (byte)(b >> 1);
            }
        }
    }
}