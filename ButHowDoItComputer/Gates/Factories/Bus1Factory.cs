using System;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates.Factories
{
    public interface IBus1Factory<TBusDataType> where TBusDataType : IBusDataType
    {
        IBus1<TBusDataType> Create(Action<TBusDataType> updateWireAction);
    }

    public class Bus1Factory<TBusDataType> : IBus1Factory<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly INot _not;
        private readonly IOr _or;
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;

        public Bus1Factory(INot not, IOr or, IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _not = not;
            _or = or;
            _busDataTypeFactory = busDataTypeFactory;
        }

        public IBus1<TBusDataType> Create(Action<TBusDataType> updateWireAction)
        {
            return new Bus1<TBusDataType>(_not, _or, _busDataTypeFactory, updateWireAction);
        }
    }
}