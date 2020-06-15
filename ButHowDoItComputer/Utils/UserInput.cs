using System.Collections.Generic;
using ButHowDoItComputer.Codes.ASCII.Interfaces;
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
        
        public List<bool[]> Input()
        {
            var input = _inputDevice.Get();

            var output = new List<bool[]>();

            for (var i = 0; i < input.Length; i++)
            {
                output.Add(_byteToAsciiConverter.ToByte(input[i].ToString()).ToBits());
            }

            return output;
        }
    }
}