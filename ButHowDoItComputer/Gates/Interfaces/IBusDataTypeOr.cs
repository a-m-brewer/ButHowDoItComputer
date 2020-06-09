using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeOr<TBusDataType> : IBusDataTypeListGate<TBusDataType> where TBusDataType : IBusDataType
    {
    }
}