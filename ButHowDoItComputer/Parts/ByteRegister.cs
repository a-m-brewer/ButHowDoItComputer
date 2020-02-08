using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class ByteRegister : IRegister<IByte>
    {
        private readonly IByteMemoryGate _byteMemoryGate;
        private readonly IByteEnabler _byteEnabler;

        public bool Enable { get; set; }
        public bool Set { get; set; }

        public IByte Input { get; set; }

        public IByte Data { get; set; }
        
        public IByte Output { get; private set; }
        public List<IBusInputSubscriber<IByte>> Subscribers { get; } =  new List<IBusInputSubscriber<IByte>>();
        public string Name { get; set; }

        public ByteRegister(IByteMemoryGate byteMemoryGate, IByteEnabler byteEnabler, IByteFactory byteFactory)
        {
            _byteMemoryGate = byteMemoryGate;
            _byteEnabler = byteEnabler;
            Input = byteFactory.Create();
            Output = byteFactory.Create();
            Data = byteFactory.Create();
            Set = false;
            Enable = false;
        }
        
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
            ApplyPrivate(input);
            ApplyOutput();
            return Output;
        }

        void IApplicable.Apply()
        {
            ApplyPrivate(Input);
            ApplyOutput();
        }

        private void ApplyPrivate(IByte input)
        {
            Data = _byteMemoryGate.Apply(input, Set);
        }

        private void ApplyOutput()
        {
            Output = _byteEnabler.Apply(Data, Enable);

            foreach (var sub in Subscribers)
            {
                sub.Input = Output;
            }
        }
    }
}