using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IByteAdder
    {
        (IByte Sum, bool CarryOut) Add(IByte a, IByte b, bool carryIn);
    }
}