using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IArithmeticLogicUnit
    {
        AluOutput Apply(IByte a, IByte b, IBit carryIn, Op op);
    }
}