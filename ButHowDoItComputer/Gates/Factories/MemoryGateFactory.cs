using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates.Factories
{
    public class MemoryGateFactory : IMemoryGateFactory
    {
        private readonly INAnd _nAnd;
        private readonly IBitFactory _bitFactory;

        public MemoryGateFactory(INAnd nAnd, IBitFactory bitFactory)
        {
            _nAnd = nAnd;
            _bitFactory = bitFactory;
        }

        public IMemoryGate Create()
        {
            return new MemoryGate(_nAnd, _bitFactory);
        }
    }
}