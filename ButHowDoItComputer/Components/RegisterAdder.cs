using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class RegisterAdder : IRegisterAdder
    {
        private readonly IByteAdder _byteAdder;

        public RegisterAdder(IByteAdder byteAdder)
        {
            _byteAdder = byteAdder;
        }
        
        public IBit Apply(IRegister<IByte> inputRegisterA, IRegister<IByte> inputRegisterB, IBit carryIn, IRegister<IByte> outputRegister)
        {
            inputRegisterA.Apply();
            inputRegisterB.Apply();

            var (outputRegisterInput, carry) = _byteAdder.Add(inputRegisterA.Output, inputRegisterB.Output, carryIn);

            outputRegister.Input = outputRegisterInput;
            outputRegister.Apply();

            return carry;
        }
    }
}