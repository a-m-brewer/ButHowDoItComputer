using System;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class CaezRegister : IRegister<Caez>
    {
        private readonly ICaezEnabler _caezEnabler;
        private readonly Action<Caez> _updateWire;
        private readonly ICaezMemoryGate _caezMemoryGate;

        public CaezRegister(ICaezMemoryGate caezMemoryGate, ICaezEnabler caezEnabler, Action<Caez> updateWire)
        {
            _caezMemoryGate = caezMemoryGate;
            _caezEnabler = caezEnabler;
            _updateWire = updateWire;

            Enable = false;
            Set = false;
            Data = new Caez {C = false, A = false, E = false, Z = false};
            Input = new Caez {C = false, A = false, E = false, Z = false};
            Output = new Caez {C = false, A = false, E = false, Z = false};
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

        public bool Enable { get; set; }
        public bool Set { get; set; }
        public Caez Data { get; set; }
        public Caez Input { get; set; }
        public Caez Output { get; private set; }

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
            Input = input;
            Apply();
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