using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates.Factories
{
    public class ByteMemoryGateFactory : IByteMemoryGateFactory
    {
        private readonly IByteFactory _byteFactory;
        private readonly IMemoryGateFactory _memoryGateFactory;

        public ByteMemoryGateFactory(IMemoryGateFactory memoryGateFactory, IByteFactory byteFactory)
        {
            _memoryGateFactory = memoryGateFactory;
            _byteFactory = byteFactory;
        }

        public IByteMemoryGate Create()
        {
            return new ByteMemoryGate(_memoryGateFactory, _byteFactory);
        }
    }
}