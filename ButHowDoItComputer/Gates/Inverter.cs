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
            var inverted = new bool[input.Count];

            for (var i = 0; i < inverted.Length; i++)
            {
                inverted[i] = _not.Apply(input[i]);
            }
            
            return _busDataTypeFactory.Create(inverted);
        }
    }
}