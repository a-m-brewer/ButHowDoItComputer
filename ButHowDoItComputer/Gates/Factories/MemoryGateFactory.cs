using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates.Factories
{
    public class MemoryGateFactory : IMemoryGateFactory
    {
        private readonly INAnd _nAnd;

        public MemoryGateFactory(INAnd nAnd)
        {
            _nAnd = nAnd;
        }

        public IMemoryGate Create()
        {
            return new MemoryGate(_nAnd);
        }
    }
}