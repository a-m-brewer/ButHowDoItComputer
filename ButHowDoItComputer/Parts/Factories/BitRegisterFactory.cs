using System;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts.Factories
{
    public class BitRegisterFactory : IRegisterFactory<bool>
    {
        private readonly IMemoryGateFactory _memoryGateFactory;

        public BitRegisterFactory(IMemoryGateFactory memoryGateFactory)
        {
            _memoryGateFactory = memoryGateFactory;
        }
        
        public IRegister<bool> Create()
        {
            return Create(action => { }, Guid.NewGuid().ToString());
        }

        public IRegister<bool> Create(Action<bool> updateOutput, string name)
        {
            return new BitRegister(_memoryGateFactory.Create(), updateOutput);
        }
    }
}