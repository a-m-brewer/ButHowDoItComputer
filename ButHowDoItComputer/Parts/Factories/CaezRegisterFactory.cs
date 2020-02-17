using System;
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
            return Create(caez => {}, Guid.NewGuid().ToString());
        }

        public IRegister<Caez> Create(Action<Caez> dataToUpdate, string name)
        {
            return new CaezRegister(new CaezMemoryGate(_memoryGateFactory), new CaezEnabler(_and), dataToUpdate) {Name = name};
        }
    }
}