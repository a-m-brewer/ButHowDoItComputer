using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBitAdder
    {
        (IBit Sum, IBit CarryOut) Add(IBit a, IBit b, IBit carryIn);
    }
}