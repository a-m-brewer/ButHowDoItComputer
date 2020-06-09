using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeAnd<TBusDataType> : IBusDataTypeListGate<TBusDataType> where TBusDataType : IBusDataType
    {
    }
}