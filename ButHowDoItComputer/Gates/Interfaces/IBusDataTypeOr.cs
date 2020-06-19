using System.Collections.Generic;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeOr<TBusDataType> : IBusDataTypeListGate<TBusDataType> where TBusDataType : IList<bool>
    {
    }
}