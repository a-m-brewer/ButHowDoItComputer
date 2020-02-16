using System;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class ByteRegister : IRegister<IByte>
    {
        private readonly IByteEnabler _byteEnabler;
        private readonly Action<IByte> _updateWire;
        private readonly IByteMemoryGate _byteMemoryGate;

        public ByteRegister(IByteMemoryGate byteMemoryGate, IByteEnabler byteEnabler, IByteFactory byteFactory, Action<IByte> updateWire)
        {
            _byteMemoryGate = byteMemoryGate;
            _byteEnabler = byteEnabler;
            _updateWire = updateWire;
            Input = byteFactory.Create();
            Output = byteFactory.Create();
            Data = byteFactory.Create();
            Set = false;
            Enable = false;
        }

        public bool Enable { get; set; }
        public bool Set { get; set; }

        public IByte Input { get; set; }

        public IByte Data { get; set; }

        public IByte Output { get; private set; }
        public string Name { get; set; }

        public IByte ApplyOnce(IByte input, bool enable = false)
        {
            Enable = enable;
            Set = true;
            var applied = Apply(input);
            Set = false;
            return applied;
        }

        public IByte Apply(IByte input)
        {
            Input = input;
            Apply();
            return Output;
        }

        public void Apply()
        {
            ApplyPrivate(Input);
            ApplyOutput();

            if (Enable)
            {
                _updateWire(Output);
            }
        }

        private void ApplyPrivate(IByte input)
        {
            Data = _byteMemoryGate.Apply(input, Set);
        }

        private void ApplyOutput()
        {
            Output = _byteEnabler.Apply(Data, Enable);
        }
    }
}