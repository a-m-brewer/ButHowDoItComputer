using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class Register : IRegister, ICpuEnableSubscriber, ICpuSettableSubscriber, IByteCpuInput
    {
        private readonly IByteMemoryGate _byteMemoryGate;
        private readonly IByteEnabler _byteEnabler;
        private readonly IBitFactory _bitFactory;

        public IBit Enable { get; set; }
        public IBit Set { get; set; }

        public IByte Input { get; set; }

        public IByte Byte { get; private set; }
        
        public IByte Output { get; private set; }

        public Register(IByteMemoryGate byteMemoryGate, IByteEnabler byteEnabler, IByteFactory byteFactory, IBitFactory bitFactory)
        {
            _byteMemoryGate = byteMemoryGate;
            _byteEnabler = byteEnabler;
            _bitFactory = bitFactory;
            Input = byteFactory.Create();
            Output = byteFactory.Create();
            Byte = byteFactory.Create();
            Set = bitFactory.Create(false);
            Enable = bitFactory.Create(false);
        }
        
        public IByte ApplyOnce(IByte input, bool enable = false)
        {
            Enable.State = enable;
            Set = _bitFactory.Create(true);
            var applied = Apply(input);
            Set = _bitFactory.Create(false);
            return applied;
        }

        public IByte Apply(IByte input)
        {
            ApplyPrivate(input);
            ApplyOutput();
            return Output;
        }

        public IByte Apply()
        {
            ApplyPrivate(Input);
            ApplyOutput();
            return Output;
        }

        void IApplicable.Apply()
        {
            Apply();
        }

        private void ApplyPrivate(IByte input)
        {
            Byte = _byteMemoryGate.Apply(input, Set);
        }

        private void ApplyOutput()
        {
            Output = _byteEnabler.Apply(Byte, Enable);
        }
    }
}