using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BusDataTypeEnabler<TBusDataType> : IBusDataTypeEnabler<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IAnd _andGate;
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;

        public BusDataTypeEnabler(IAnd andGate, IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _andGate = andGate;
            _busDataTypeFactory = busDataTypeFactory;
        }

        public TBusDataType Apply(TBusDataType input, bool set)
        {
            var bits = new bool[input.Count];

            for (var i = 0; i < bits.Length; i++)
            {
                bits[i] = _andGate.ApplyParams(input[i], set);
            }
            
            return _busDataTypeFactory.Create(bits);
        }
    }
}