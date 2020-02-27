using System.Linq;
using ButHowDoItComputer.Codes.ASCII.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Utils
{
    public class UserInput : IUserInput
    {
        private readonly IInputDevice _inputDevice;
        private readonly IByteToAsciiConverter _byteToAsciiConverter;

        public UserInput(IInputDevice inputDevice, IByteToAsciiConverter byteToAsciiConverter)
        {
            _inputDevice = inputDevice;
            _byteToAsciiConverter = byteToAsciiConverter;
        }
        
        public IByte[] Input()
        {
            var input = _inputDevice.Get();
            return input.Select(c => _byteToAsciiConverter.ToByte(c.ToString())).ToArray();
        }
    }
}