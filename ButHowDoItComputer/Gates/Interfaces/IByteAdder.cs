using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IByteAdder
    {
        (IByte Sum, IBit CarryOut) Add(IByte a, IByte b, IBit carryIn);
    }
}