using System.Collections.Generic;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class CaezRegister : IRegister<Caez>
    {
        private readonly ICaezMemoryGate _caezMemoryGate;
        private readonly ICaezEnabler _caezEnabler;

        public CaezRegister(ICaezMemoryGate caezMemoryGate, ICaezEnabler caezEnabler)
        {
            _caezMemoryGate = caezMemoryGate;
            _caezEnabler = caezEnabler;

            Enable = false;
            Set = false;
            Data = new Caez { C = false, A = false, E = false, Z = false};
            Input = new Caez { C = false, A = false, E = false, Z = false};
            Output = new Caez { C = false, A = false, E = false, Z = false};
        }
        
        public void Apply()
        {
            ApplyPrivate(Input);
            ApplyOutput();
        }

        public bool Enable { get; set; }
        public bool Set { get; set; }
        public Caez Data { get; set; }
        public Caez Input { get; set; }
        public Caez Output { get; private set; }
        
        public List<IBusInputSubscriber<Caez>> Subscribers { get; } = new List<IBusInputSubscriber<Caez>>();
        
        public string Name { get; set; }

        public Caez ApplyOnce(Caez input, bool enable = false)
        {
            Enable = enable;
            Set = true;
            var applied = Apply(input);
            Set = false;
            return applied;
        }

        public Caez Apply(Caez input)
        {
            ApplyPrivate(input);
            ApplyOutput();
            return Output;
        }
        
        private void ApplyPrivate(Caez input)
        {
            Data = _caezMemoryGate.Apply(input, Set);
        }
        
        private void ApplyOutput()
        {
            Output = _caezEnabler.Apply(Data, Enable);

            foreach (var subscriber in Subscribers)
            {
                subscriber.Input = Output;
            }
        }

        Caez IBusInputSubscriber<Caez>.Input { get; set; }
    }
}