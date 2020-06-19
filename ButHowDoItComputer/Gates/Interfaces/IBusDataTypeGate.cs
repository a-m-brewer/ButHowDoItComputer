using System.Collections.Generic;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeGate<TBusDataType> : IGenericMemoryGate<TBusDataType> where TBusDataType : IList<bool>
    {
    }
}