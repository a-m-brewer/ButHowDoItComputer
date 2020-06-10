using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeGate<TBusDataType> : IGenericMemoryGate<TBusDataType> where TBusDataType : IBusDataType
    {
    }
}