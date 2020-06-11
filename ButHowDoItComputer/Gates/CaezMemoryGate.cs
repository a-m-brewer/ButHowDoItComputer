using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class CaezMemoryGate : ICaezMemoryGate
    {
        private readonly List<IMemoryGate> _memoryGates;

        public CaezMemoryGate(IMemoryGateFactory memoryGateFactory)
        {
            _memoryGates = Enumerable.Range(0, 8).Select(_ => memoryGateFactory.Create()).ToList();
        }

        public Caez Apply(Caez input, bool set)
        {
            var newState = new bool[4];
            for (var i = 0; i < input.Count; i++)
            {
                newState[i] = _memoryGates[i].Apply(input[i], set);
            }

            return new Caez(newState);
        }
    }
}