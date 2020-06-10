using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeMemoryGateFactory<TBusDataType> where TBusDataType : IBusDataType
    {
        IBusDataTypeMemoryGate<TBusDataType> Create();
    }
}