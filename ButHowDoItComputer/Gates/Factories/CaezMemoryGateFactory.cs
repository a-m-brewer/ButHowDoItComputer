using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates.Factories
{
    public class CaezMemoryGateFactory : ICaezMemoryGateFactory
    {
        private readonly IMemoryGateFactory _memoryGateFactory;

        public CaezMemoryGateFactory(IMemoryGateFactory memoryGateFactory)
        {
            _memoryGateFactory = memoryGateFactory;
        }
        
        public ICaezMemoryGate Create()
        {
            return new CaezMemoryGate(_memoryGateFactory);
        }
    }
}