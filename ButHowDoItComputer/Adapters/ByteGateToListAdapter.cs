using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Adapters
{
    class ByteGateToListAdapter : IByteListGate
    {
        private readonly IByteGate _byteGate;
        private readonly IBit _set;

        public ByteGateToListAdapter(IByteGate byteGate, IBit set)
        {
            _byteGate = byteGate;
            _set = set;
        }

        public IByte Apply(params IByte[] input)
        {
            return _byteGate.Apply(input[0], _set);
        }
    }
}
