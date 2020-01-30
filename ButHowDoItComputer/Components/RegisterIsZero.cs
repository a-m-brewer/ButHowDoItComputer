using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    class RegisterIsZero : IRegisterIsZero
    {
        private readonly IIsZeroGate _isZeroGate;

        public RegisterIsZero(IIsZeroGate isZeroGate)
        {
            _isZeroGate = isZeroGate;
        }

        public IBit IsZero(IRegister<IByte> inputRegister)
        {
            inputRegister.Apply();
            return _isZeroGate.IsZero(inputRegister.Output);
        }
    }
}
