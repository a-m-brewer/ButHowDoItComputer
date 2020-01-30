using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.Components
{
    public class RightShifter : Shifter, IRightShifter
    {
        private readonly IRightByteShifter _rightByteShifter;

        public RightShifter(ByteFactory byteFactory, IBitFactory bitFactory, IRightByteShifter rightByteShifter) : base(byteFactory, bitFactory)
        {
            _rightByteShifter = rightByteShifter;
        }

        protected override IBit[] GetShifter(IRegister<IByte> inputRegister)
        {
            var (secondRegisterInput, shiftOut) = _rightByteShifter.Shift(inputRegister.Output, ShiftIn);
            ShiftOut = shiftOut;
            
            return secondRegisterInput.ToBits();
        }
    }
}