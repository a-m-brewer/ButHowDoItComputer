using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface ISingleInputGate
    {
        IBit Apply(IBit bit);
    }
}