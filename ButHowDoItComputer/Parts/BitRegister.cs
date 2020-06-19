using System;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class BitRegister : IRegister<bool>
    {
        private readonly IMemoryGate _memoryGate;
        private readonly Action<bool> _updateWire;

        public BitRegister(IMemoryGate memoryGate, Action<bool> updateWire)
        {
            _memoryGate = memoryGate;
            _updateWire = updateWire;
        }
        
        public void Apply()
        {
            Data = _memoryGate.Apply(Input, Set);
            Output = Data && Enable;

            if (Enable)
            {
                _updateWire(Output);
            }
        }

        public bool Enable { get; set; }
        public bool Set { get; set; }
        public string Name { get; set; }
        public bool Data { get; set; }
        public bool Input { get; set; }
        public bool Output { get; private set; }
        
        public bool ApplyOnce(bool input, bool enable = false)
        {
            Enable = enable;
            Set = true;
            Apply(input);
            Set = false;
            return Output;
        }

        public bool Apply(bool input)
        {
            Input = input;
            Apply();
            return Output;
        }
    }
}