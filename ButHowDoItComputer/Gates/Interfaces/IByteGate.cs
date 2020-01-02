using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IByteGate
    {
        IByte Apply(IByte input, IBit set);
    }
}