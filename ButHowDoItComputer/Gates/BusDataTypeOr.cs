using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BusDataTypeOr<TBusDataType> : BaseBusDataTypeGate<TBusDataType>, IBusDataTypeOr<TBusDataType> where TBusDataType : IBusDataType
    {
        public BusDataTypeOr(IOr or, IBusDataTypeFactory<TBusDataType> busDataTypeFactory) : base(or, busDataTypeFactory)
        {
        }
    }
}