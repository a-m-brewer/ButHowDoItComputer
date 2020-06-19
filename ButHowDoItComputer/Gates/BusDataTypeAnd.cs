using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.Gates
{
    public class BusDataTypeAnd<TBusDataType> : IBusDataTypeAnd<TBusDataType> where TBusDataType : IList<bool>
    {
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;

        public BusDataTypeAnd(IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _busDataTypeFactory = busDataTypeFactory;
        }

        public TBusDataType Apply(params TBusDataType[] input)
        {
            var result = new bool[input[0].Count];

            for (var i = 0; i < result.Length; i++)
            {
                var toOr = new bool[input.Length];

                for (var j = 0; j < toOr.Length; j++)
                {
                    toOr[j] = input[j][i];
                }

                result[i] = toOr.AndList();
            }

            return _busDataTypeFactory.CreateParams(result);  
        }
    }
}