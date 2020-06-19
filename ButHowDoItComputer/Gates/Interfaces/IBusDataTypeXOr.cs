using System.Collections.Generic;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeXOr<TBusDataType> : IBusDataTypeListGate<TBusDataType> where TBusDataType : IList<bool>
    {
    }
}