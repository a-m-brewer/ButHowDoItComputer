using ButHowDoItComputer.Components.Interfaces;
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
        
        public void Apply(IRegister inputRegister, IRegister outputRegister)
        {
            inputRegister.Apply();
            var outputRegisterInput = _inverter.Invert(inputRegister.Output);
            outputRegister.Input = outputRegisterInput;
            outputRegister.Apply();
        }
    }

    public interface IRegisterInverter : IRegisterTransfer { }
}