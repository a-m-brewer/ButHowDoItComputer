using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class CaezMemoryGate : ICaezMemoryGate
    {
        private List<IMemoryGate> _memoryGates;

        public CaezMemoryGate(IMemoryGateFactory memoryGateFactory)
        {
            _memoryGates = Enumerable.Range(0, 8).Select(_ => memoryGateFactory.Create()).ToList();
        }
        
        public Caez Apply(Caez input, IBit set)
        {
            var newState = new IBit[4];
            var listCaez = input.ToList();
            for (var i = 0; i < listCaez.Count; i++)
            {
                newState[i] = _memoryGates[i].Apply(listCaez[i], set);
            }
            
            return new Caez
            {
                C = newState[0],
                A = newState[1],
                E = newState[2],
                Z = newState[3]
            };
        }
    }
}