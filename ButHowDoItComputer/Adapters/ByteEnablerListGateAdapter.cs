using ButHowDoItComputer.Adapters.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Adapters
{
    public class ByteEnablerListGateAdapter : IByteEnablerListGateAdapter
    {
        private readonly IByteEnabler _byteEnabler;
        private readonly IBit _set;

        public ByteEnablerListGateAdapter(IByteEnabler byteEnabler, IBit set)
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