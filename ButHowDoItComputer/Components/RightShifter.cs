using ButHowDoItComputer.DataTypes.Enums;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class RightShifter : Shifter
    {
        public RightShifter(ByteFactory byteFactory, IBitFactory bitFactory) : base(byteFactory, bitFactory)
        {
        }

        protected override IBit[] GetShifter(IRegister inputRegister)
        {
            ShiftOut = inputRegister.Output[0];
            var secondRegisterInput = new[]
            {
                inputRegister.Output[1],
                inputRegister.Output[2],
                inputRegister.Output[3],
                inputRegister.Output[4],
                inputRegister.Output[5],
                inputRegister.Output[6],
                inputRegister.Output[7],
                ShiftIn
            };
            return secondRegisterInput;
        }
    }
}