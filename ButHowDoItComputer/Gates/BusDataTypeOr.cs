using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BusDataTypeOr<TBusDataType> : IBusDataTypeOr<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;
        private readonly IOr _or;

        public BusDataTypeOr(IOr or, IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _or = or;
            _busDataTypeFactory = busDataTypeFactory;
        }

        public TBusDataType Apply(params TBusDataType[] input)
        {
            var groups = new List<List<bool>>();
            for (var i = 0; i < input[0].Count; i++)
            {
                var tempList = input.Select(t => t[i]).ToList();
                groups.Add(tempList);
            }

            var result = groups.Select(s => _or.Apply(s)).ToArray();

            return _busDataTypeFactory.CreateParams(result);
        }
    }
}