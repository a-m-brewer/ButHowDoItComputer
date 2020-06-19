using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BusDataTypeEnabler<TBusDataType> : IBusDataTypeEnabler<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;

        public BusDataTypeEnabler(IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _busDataTypeFactory = busDataTypeFactory;
        }

        public TBusDataType Apply(TBusDataType input, bool set)
        {
            var bits = new bool[input.Count];

            for (var i = 0; i < bits.Length; i++)
            {
                bits[i] = input[i] && set;
            }
            
            return _busDataTypeFactory.CreateParams(bits);
        }
    }
}