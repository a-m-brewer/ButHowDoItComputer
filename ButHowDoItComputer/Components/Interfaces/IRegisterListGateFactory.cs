using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IRegisterListGateFactory
    {
        IRegisterListGate Create(IByteListGate byteListGate);
    }
}