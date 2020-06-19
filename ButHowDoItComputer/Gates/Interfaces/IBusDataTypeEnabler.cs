using System.Collections.Generic;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeEnabler<TBusDataType> : IBusDataTypeGate<TBusDataType> where TBusDataType : IList<bool>
    {
    }
}