using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class RegisterInverter : IRegisterInverter
    {
        private readonly IInverter _inverter;

        public RegisterInverter(IInverter inverter)
        {
            _inverter = inverter;
        }
        
        public void Apply(IRegister<IByte> inputRegister, IRegister<IByte> outputRegister)
        {
            inputRegister.Apply();
            var outputRegisterInput = _inverter.Invert(inputRegister.Output);
            outputRegister.Input = outputRegisterInput;
            outputRegister.Apply();
        }
    }
}