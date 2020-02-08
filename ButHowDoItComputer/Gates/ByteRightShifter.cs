using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class ByteRightShifter : IRightByteShifter
    {
        private readonly IByteFactory _byteFactory;

        public ByteRightShifter(IByteFactory byteFactory)
        {
            _byteFactory = byteFactory;
        }

        public (IByte Ouput, bool ShiftOut) Shift(IByte input, bool shiftIn)
        {
            var shift = new[] 
            {
                input[1],
                input[2],
                input[3],
                input[4],
                input[5],
                input[6],
                input[7],
                shiftIn
            };
            return (_byteFactory.Create(shift), input[0]);
        }
    }
}
