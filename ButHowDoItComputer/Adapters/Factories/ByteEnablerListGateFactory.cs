using ButHowDoItComputer.Adapters.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Adapters.Factories
{
    public class ByteEnablerListGateFactory : IByteEnablerListGateFactory
    {
        private readonly IByteEnabler _byteEnabler;

        public ByteEnablerListGateFactory(IByteEnabler byteEnabler)
        {
            _byteEnabler = byteEnabler;
        }

        public IByteEnablerListGateAdapter Create(IBit set)
        {
            return new ByteEnablerListGateAdapter(_byteEnabler, set);
        }
    }
}