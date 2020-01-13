using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts.Factories
{
    public class RegisterFactory : IRegisterFactory
    {
        private readonly IByteEnabler _byteEnabler;
        private readonly IByteFactory _byteFactory;
        private readonly IBitFactory _bitFactory;
        private readonly IByteMemoryGateFactory _byteMemoryGateFactory;

        public RegisterFactory(IByteMemoryGateFactory byteMemoryGateFactory, IByteEnabler byteEnabler, IByteFactory byteFactory, IBitFactory bitFactory)
        {
            _byteMemoryGateFactory = byteMemoryGateFactory;
            _byteEnabler = byteEnabler;
            _byteFactory = byteFactory;
            _bitFactory = bitFactory;
        }
        
        public IRegister Create()
        {
            return new Register(_byteMemoryGateFactory.Create(), _byteEnabler, _byteFactory, _bitFactory);
        }
    }
}