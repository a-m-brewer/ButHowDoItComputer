using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates.Factories
{
    public class BusDataTypeMemoryGateFactory<TBusDataType> : IBusDataTypeMemoryGateFactory<TBusDataType> where TBusDataType : IList<bool>
    {
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;
        private readonly int _bits;
        private readonly IMemoryGateFactory _memoryGateFactory;
        private NAnd _nand;

        public BusDataTypeMemoryGateFactory(IMemoryGateFactory memoryGateFactory, IBusDataTypeFactory<TBusDataType> busDataTypeFactory, int bits)
        {
            _memoryGateFactory = memoryGateFactory;
            _busDataTypeFactory = busDataTypeFactory;
            _bits = bits;
            _nand = new NAnd(new Not());
        }

        public IBusDataTypeMemoryGate<TBusDataType> Create()
        {
            return new BusDataTypeMemoryGate<TBusDataType>(_nand, _busDataTypeFactory, _bits);
        }
    }
}