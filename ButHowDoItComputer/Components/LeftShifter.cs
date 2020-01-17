using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.Components
{
    public class LeftShifter : Shifter, ILeftShifter
    {
        private readonly ILeftByteShifter _leftByteShifter;

        public LeftShifter(ByteFactory byteFactory, IBitFactory bitFactory, ILeftByteShifter leftByteShifter) : base(byteFactory, bitFactory)
        {
            _leftByteShifter = leftByteShifter;
        }

        protected override IBit[] GetShifter(IRegister inputRegister)
        {
            var (ouput, shiftOut) = _leftByteShifter.Shift(inputRegister.Output, ShiftIn);
            ShiftOut = shiftOut;
            return ouput.ToBits();
        }
    }
}