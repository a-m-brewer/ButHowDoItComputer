using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts.Factories
{
    public class CaezRegisterFactory : ICaezRegisterFactory
    {
        private readonly IAnd _and;
        private readonly IMemoryGateFactory _memoryGateFactory;

        public CaezRegisterFactory(IMemoryGateFactory memoryGateFactory, IAnd and)
        {
            _memoryGateFactory = memoryGateFactory;
            _and = and;
        }

        public IRegister<Caez> Create()
        {
            return new CaezRegister(new CaezMemoryGate(_memoryGateFactory), new CaezEnabler(_and));
        }
    }
}