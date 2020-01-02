using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class ByteMemoryGate : IByteMemoryGate
    {
        private readonly IByteFactory _byteFactory;
        private readonly List<IMemoryGate> _memoryGates;

        public ByteMemoryGate(IMemoryGateFactory memoryGateFactory, IByteFactory byteFactory)
        {
            _byteFactory = byteFactory;
            _memoryGates = Enumerable.Range(0, 8).Select(_ => memoryGateFactory.Create()).ToList();
        }
        
        public IByte Apply(IByte input, IBit set)
        {
            var newState = new IBit[8];
            for (var i = 0; i < input.Count; i++)
            {
                newState[i] = _memoryGates[i].Apply(input[i], set);
            }

            return _byteFactory.Create(newState);
        }
    }
}