using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class AluWire<TBusDataType> : IAluWire<TBusDataType> where TBusDataType : IList<bool>
    {
        private readonly IBusDataTypeFactory<TBusDataType> _byteFactory;

        public AluWire(IBusDataTypeFactory<TBusDataType> byteFactory)
        {
            _byteFactory = byteFactory;
        }

        public TBusDataType Apply(params TBusDataType[] input)
        {
            return input.LastOrDefault(w => w.Any(a => a)) ?? _byteFactory.Create();
        }
    }
}