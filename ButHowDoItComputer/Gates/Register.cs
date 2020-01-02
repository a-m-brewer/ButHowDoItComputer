using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class Register : IRegister
    {
        private readonly IByteMemoryGate _byteMemoryGate;
        private readonly IByteEnabler _byteEnabler;

        public IBit Enable { get; set; }
        public IBit Set { get; set; }

        public IByte Byte { get; private set; }

        public Register(IByteMemoryGate byteMemoryGate, IByteEnabler byteEnabler, IByteFactory byteFactory, IBitFactory bitFactory)
        {
            _byteMemoryGate = byteMemoryGate;
            _byteEnabler = byteEnabler;
            Byte = byteFactory.Create();
            Set = bitFactory.Create(false);
            Enable = bitFactory.Create(false);
        }
        
        public IByte Apply(IByte input, IBit set, IBit enable)
        {
            Set = set;
            Enable = enable;

            return Apply(input);
        }

        public IByte Apply(IByte input)
        {
            ApplyPrivate(input);
            return _byteEnabler.Apply(Byte, Enable);
        }

        private void ApplyPrivate(IByte input)
        {
            Byte = _byteMemoryGate.Apply(input, Set);
        }
    }
}