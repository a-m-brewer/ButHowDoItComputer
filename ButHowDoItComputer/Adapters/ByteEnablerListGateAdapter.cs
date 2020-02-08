using ButHowDoItComputer.Adapters.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Adapters
{
    public class ByteEnablerListGateAdapter : IByteEnablerListGateAdapter
    {
        private readonly IByteEnabler _byteEnabler;
        private readonly bool _set;

        public ByteEnablerListGateAdapter(IByteEnabler byteEnabler, bool set)
        {
            _byteEnabler = byteEnabler;
            _set = set;
        }

        public IByte Apply(params IByte[] input)
        {
            return _byteEnabler.Apply(input[0], _set);
        }
    }
}