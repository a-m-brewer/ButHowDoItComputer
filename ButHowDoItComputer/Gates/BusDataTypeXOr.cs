using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BusDataTypeXOr<TBusDataType> : IBusDataTypeXOr<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;
        private readonly IXOr _xOr;

        public BusDataTypeXOr(IXOr xOr, IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _xOr = xOr;
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

            var result = groups.Select(s => _xOr.Apply(s.ToArray())).ToArray();

            return _busDataTypeFactory.Create(result);
        }
    }
}