using System;
using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts.Factories
{
    public class BusDataTypeRegisterFactory<TBusDataType> : IBusDataTypeRegisterFactory<TBusDataType> where TBusDataType : IList<bool>
    {
        private readonly IBusDataTypeEnabler<TBusDataType> _busDataTypeEnabler;
        private readonly IBusDataTypeFactory<TBusDataType> _byteFactory;
        private readonly IBusDataTypeMemoryGateFactory<TBusDataType> _busDataTypeMemoryGateFactory;

        public BusDataTypeRegisterFactory(IBusDataTypeMemoryGateFactory<TBusDataType> busDataTypeMemoryGateFactory, IBusDataTypeEnabler<TBusDataType> busDataTypeEnabler,
            IBusDataTypeFactory<TBusDataType> byteFactory)
        {
            _busDataTypeMemoryGateFactory = busDataTypeMemoryGateFactory;
            _busDataTypeEnabler = busDataTypeEnabler;
            _byteFactory = byteFactory;
        }

        public IRegister<TBusDataType> Create()
        {
            return Create(update => {}, Guid.NewGuid().ToString());
        }

        public IRegister<TBusDataType> Create(Action<TBusDataType> dataToUpdate, string name)
        {
            var reg = new BusDataTypeRegister<TBusDataType>(_busDataTypeMemoryGateFactory.Create(), _busDataTypeEnabler, _byteFactory, dataToUpdate)
            {
                Name = name
            };
            
            return reg;
        }
    }
}