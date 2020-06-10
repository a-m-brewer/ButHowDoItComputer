using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates.Factories
{
    public class BusDataTypeMemoryGateFactory<TBusDataType> : IBusDataTypeMemoryGateFactory<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;
        private readonly IMemoryGateFactory _memoryGateFactory;

        public BusDataTypeMemoryGateFactory(IMemoryGateFactory memoryGateFactory, IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _memoryGateFactory = memoryGateFactory;
            _busDataTypeFactory = busDataTypeFactory;
        }

        public IBusDataTypeMemoryGate<TBusDataType> Create()
        {
            return new BusDataTypeMemoryGate<TBusDataType>(_memoryGateFactory, _busDataTypeFactory);
        }
    }
}