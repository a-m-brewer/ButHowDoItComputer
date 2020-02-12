using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class ByteLeftShifter : ILeftByteShifter
    {
        private readonly IByteFactory _byteFactory;

        public ByteLeftShifter(IByteFactory byteFactory)
        {
            _byteFactory = byteFactory;
        }

        public (IByte Ouput, bool ShiftOut) Shift(IByte input, bool shiftIn)
        {
            var shift = new[]
            {
                shiftIn,
                input[0],
                input[1],
                input[2],
                input[3],
                input[4],
                input[5],
                input[6]
            };

            return (_byteFactory.Create(shift), input[7]);
        }
    }
}