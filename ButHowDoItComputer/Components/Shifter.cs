using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class Shifter : IShifter
    {
        private readonly ByteFactory _byteFactory;

        protected Shifter(ByteFactory byteFactory)
        {
            _byteFactory = byteFactory;
            ShiftIn = false;
            ShiftOut = false;
        }

        public bool ShiftIn { get; set; }
        public bool ShiftOut { get; set; }

        public void Apply(IRegister<IByte> inputRegister, IRegister<IByte> outputRegister)
        {
            inputRegister.Apply();
            var secondRegisterInput = GetShifter(inputRegister);
            outputRegister.Input = _byteFactory.Create(secondRegisterInput);
            outputRegister.Apply();
        }

        protected virtual bool[] GetShifter(IRegister<IByte> inputRegister)
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