using ButHowDoItComputer.Adapters.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Adapters.Factories
{
    public class ByteGateToListFactory : IByteGateToListFactory
    {
        public IByteListGate Convert(IByteGate byteGate, IBit set)
        {
            return new ByteGateToListAdapter(byteGate, set);
        }
    }
}
