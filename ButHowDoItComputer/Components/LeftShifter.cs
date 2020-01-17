using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class LeftShifter : Shifter, ILeftShifter
    {
        public LeftShifter(ByteFactory byteFactory, IBitFactory bitFactory) : base(byteFactory, bitFactory)
        {
        }

        protected override IBit[] GetShifter(IRegister inputRegister)
        {
            ShiftOut = inputRegister.Output[7];
            var secondRegisterInput = new[]
            {
                ShiftIn,
                inputRegister.Output[0],
                inputRegister.Output[1],
                inputRegister.Output[2],
                inputRegister.Output[3],
                inputRegister.Output[4],
                inputRegister.Output[5],
                inputRegister.Output[6]
            };
            return secondRegisterInput;
        }
    }
}