using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BusDataTypeMemoryGate<TBusDataType> : IBusDataTypeMemoryGate<TBusDataType> where TBusDataType : IList<bool>
    {
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;
        private readonly List<IMemoryGate> _memoryGates;

        public BusDataTypeMemoryGate(IMemoryGateFactory memoryGateFactory, IBusDataTypeFactory<TBusDataType> busDataTypeFactory, int bits)
        {
            _busDataTypeFactory = busDataTypeFactory;
            _memoryGates = Enumerable.Range(0, bits).Select(_ => memoryGateFactory.Create()).ToList();
        }

        public TBusDataType Apply(TBusDataType input, bool set)
        {
            var bits = new bool[input.Count];

            for (var i = 0; i < bits.Length; i++)
            {
                bits[i] = _memoryGates[i].Apply(input[i], set);
            }

            return _busDataTypeFactory.CreateParams(bits);
        }
    }
}