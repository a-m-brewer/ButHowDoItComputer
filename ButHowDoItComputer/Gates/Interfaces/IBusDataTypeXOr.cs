using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeXOr<TBusDataType> : IBusDataTypeListGate<TBusDataType> where TBusDataType : IBusDataType
    {
    }
}