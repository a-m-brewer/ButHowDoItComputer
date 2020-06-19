using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class BusDataTypeXOr<TBusDataType> : BaseBusDataTypeGate<TBusDataType>, IBusDataTypeXOr<TBusDataType> where TBusDataType : IList<bool>
    {
        public BusDataTypeXOr(IXOr xOr, IBusDataTypeFactory<TBusDataType> busDataTypeFactory) : base(xOr, busDataTypeFactory)
        {
        }
    }
}