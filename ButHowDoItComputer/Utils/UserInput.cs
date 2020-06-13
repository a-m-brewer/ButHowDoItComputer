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

            var output = new IByte[input.Length];

            for (var i = 0; i < output.Length; i++)
            {
                output[i] = _byteToAsciiConverter.ToByte(input[i].ToString());
            }

            return output;
        }
    }
}