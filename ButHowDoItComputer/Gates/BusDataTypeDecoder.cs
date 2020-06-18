using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BusDataTypeDecoder<TBusDataType> : IBusDataTypeDecoder<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;
        private readonly IDecoder _decoder;

        public BusDataTypeDecoder(IDecoder decoder, IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            _decoder = decoder;
            _busDataTypeFactory = busDataTypeFactory;
        }

        public TBusDataType Decode(bool a, bool b, bool c)
        {
            return _busDataTypeFactory.Create(_decoder.ApplyParams(a, b, c));
        }
    }
}