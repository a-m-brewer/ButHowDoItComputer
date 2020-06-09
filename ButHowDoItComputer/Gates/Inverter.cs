using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class Inverter<TBusDataType> : IInverter<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;
        private readonly INot _not;

        public Inverter(INot not, IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _not = not;
            _busDataTypeFactory = busDataTypeFactory;
        }

        public TBusDataType Invert(TBusDataType input)
        {
            return _busDataTypeFactory.Create(input.Select(s => _not.Apply(s)).ToArray());
        }
    }
}