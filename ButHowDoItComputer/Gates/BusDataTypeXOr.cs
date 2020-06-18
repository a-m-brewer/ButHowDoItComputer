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
            var groups = new bool[input[0].Count][];
            for (var i = 0; i < input.Length; i++)
            {
                var tmpList = new bool[input[i].Count];
                
                for (var j = 0; j < input[i].Count; j++)
                {
                    tmpList[j] = input[i][j];
                }
                
                groups[i] = tmpList;
            }

            var result = new bool[groups.Length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = _xOr.Apply(groups[i]);
            }

            return _busDataTypeFactory.CreateParams(result);
        }
    }
}