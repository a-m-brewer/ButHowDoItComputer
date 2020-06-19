using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeMemoryGateFactory<TBusDataType> where TBusDataType : IList<bool>
    {
        IBusDataTypeMemoryGate<TBusDataType> Create();
    }
}