using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Adapters.Interfaces
{
    public interface IByteEnablerListGateFactory
    {
        IByteEnablerListGateAdapter Create(IBit set);
    }
}