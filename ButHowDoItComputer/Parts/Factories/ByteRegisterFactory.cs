using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts.Factories
{
    public class ByteRegisterFactory : IByteRegisterFactory
    {
        private readonly IByteEnabler _byteEnabler;
        private readonly IByteFactory _byteFactory;
        private readonly IByteMemoryGateFactory _byteMemoryGateFactory;

        public ByteRegisterFactory(IByteMemoryGateFactory byteMemoryGateFactory, IByteEnabler byteEnabler, IByteFactory byteFactory)
        {
            _byteMemoryGateFactory = byteMemoryGateFactory;
            _byteEnabler = byteEnabler;
            _byteFactory = byteFactory;
        }
        
        public IRegister<IByte> Create()
        {
            return new ByteRegister(_byteMemoryGateFactory.Create(), _byteEnabler, _byteFactory);
        }
    }
}