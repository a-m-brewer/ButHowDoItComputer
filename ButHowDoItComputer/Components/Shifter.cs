using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class Shifter : IShifter
    {
        private readonly ByteFactory _byteFactory;
        public IBit ShiftIn { get; set; }
        public IBit ShiftOut { get; set; }

        protected Shifter(ByteFactory byteFactory, IBitFactory bitFactory)
        {
            _byteFactory = byteFactory;
            ShiftIn = bitFactory.Create(false);
            ShiftOut = bitFactory.Create(false);
        }
        
        public void Apply(IRegister inputRegister, IRegister outputRegister)
        {
            inputRegister.Apply();
            var secondRegisterInput = GetShifter(inputRegister);
            outputRegister.Input = _byteFactory.Create(secondRegisterInput);
            outputRegister.Apply();
        }

        protected virtual IBit[] GetShifter(IRegister inputRegister)
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