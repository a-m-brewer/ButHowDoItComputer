using ButHowDoItComputer.Adapters.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Adapters.Factories
{
    public class ByteGateToListFactory : IByteGateToListFactory
    {
        public IByteListGate Convert(IByteGate byteGate, bool set)
        {
            return new ByteGateToListAdapter(byteGate, set);
        }
    }
}