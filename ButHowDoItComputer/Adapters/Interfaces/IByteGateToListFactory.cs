using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Adapters.Interfaces
{
    public interface IByteGateToListFactory
    {
        IByteListGate Convert(IByteGate byteGate, bool set);
    }
}