using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

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
            
            Enable = new Bit(false);
            Set = new Bit(false);
            Data = new Caez();
            Input = new Caez();
            Output = new Caez();
        }
        
        public void Apply()
        {
            ApplyPrivate(Input);
            ApplyOutput();
        }

        public IBit Enable { get; set; }
        public IBit Set { get; set; }
        public Caez Data { get; private set; }
        public Caez Input { get; set; }
        public Caez Output { get; private set; }
        
        public Caez ApplyOnce(Caez input, bool enable = false)
        {
            Enable.State = enable;
            Set.State = true;
            var applied = Apply(input);
            Set.State = false;
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
        }
    }
}