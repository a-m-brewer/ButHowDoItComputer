using System.Collections.Generic;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeMemoryGateFactory<TBusDataType> where TBusDataType : IList<bool>
    {
        IBusDataTypeMemoryGate<TBusDataType> Create();
    }
}