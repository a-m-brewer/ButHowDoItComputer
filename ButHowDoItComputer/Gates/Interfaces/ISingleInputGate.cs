using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface ISingleInputGate
    {
        bool Apply(bool bit);
    }
}