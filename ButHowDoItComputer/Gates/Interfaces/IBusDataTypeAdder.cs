using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeAdder<TBusDataType> where TBusDataType : IBusDataType
    {
        (TBusDataType Sum, bool CarryOut) Add(TBusDataType a, TBusDataType b, bool carryIn);
    }
}