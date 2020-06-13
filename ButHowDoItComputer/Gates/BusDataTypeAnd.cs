using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BusDataTypeAnd<TBusDataType> : IBusDataTypeAnd<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IAnd _and;
        private readonly IBusDataTypeFactory<TBusDataType> _byteFactory;

        public BusDataTypeAnd(IAnd and, IBusDataTypeFactory<TBusDataType> byteFactory)
        {
            _and = and;
            _byteFactory = byteFactory;
        }

        public TBusDataType Apply(params TBusDataType[] input)
        {
            var groups = new List<List<bool>>();
            
            for (var i = 0; i < input[0].Count; i++)
            {
                var tempList = input.Select(t => t[i]).ToList();
                groups.Add(tempList);
            }

            var result = groups.Select(s => _and.Apply(s)).ToArray();

            return _byteFactory.CreateParams(result);
        }
    }
}