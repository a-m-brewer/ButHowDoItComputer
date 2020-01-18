using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Components.Factories
{
    public class RegisterListGateFactory : IRegisterListGateFactory
    {
        public IRegisterListGate Create(IByteListGate byteListGate)
        {
            return new RegisterListGate(byteListGate);
        }
    }
}