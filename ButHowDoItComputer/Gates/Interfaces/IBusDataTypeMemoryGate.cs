using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeMemoryGate<TBusDataType> : IBusDataTypeGate<TBusDataType> where TBusDataType : IBusDataType
    {
    }
}