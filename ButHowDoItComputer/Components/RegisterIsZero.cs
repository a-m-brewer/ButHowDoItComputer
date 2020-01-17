using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
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

        public IBit IsZero(IRegister inputRegister)
        {
            inputRegister.Apply();
            return _isZeroGate.IsZero(inputRegister.Output);
        }
    }

    public interface IRegisterIsZero
    {
        IBit IsZero(IRegister inputRegister);
    }
}
