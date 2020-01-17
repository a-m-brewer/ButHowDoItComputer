using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    class RegisterArithmeticLogicUnit
    {
        private readonly IArithmeticLogicUnit _arithmeticLogicUnit;

        public RegisterArithmeticLogicUnit(IArithmeticLogicUnit arithmeticLogicUnit)
        {
            _arithmeticLogicUnit = arithmeticLogicUnit;
        }

        public AluOutput Apply(IRegister a, IRegister b, IBit carryIn, Op op, IRegister outputRegister)
        {
            a.Apply();
            b.Apply();

            var result = _arithmeticLogicUnit.Apply(a.Output, b.Output, carryIn, op);

            outputRegister.Input = result.Output;
            outputRegister.Apply();

            return result;
        }
    }
}
