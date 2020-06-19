using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BusDataTypeAnd<TBusDataType> : BaseBusDataTypeGate<TBusDataType>, IBusDataTypeAnd<TBusDataType> where TBusDataType : IBusDataType
    {
        public BusDataTypeAnd(IAnd and, IBusDataTypeFactory<TBusDataType> byteFactory) : base(and, byteFactory)
        {
        }
    }
}