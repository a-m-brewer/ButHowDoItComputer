using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IByteListGate
    {
        IByte Apply(params IByte[] input);
    }
}