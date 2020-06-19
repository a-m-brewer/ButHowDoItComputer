using System.Collections.Generic;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeAnd<TBusDataType> : IBusDataTypeListGate<TBusDataType> where TBusDataType : IList<bool>
    {
    }
}