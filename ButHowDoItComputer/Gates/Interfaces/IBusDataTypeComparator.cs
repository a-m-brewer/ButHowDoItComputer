using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeComparator<TBusDataType> where TBusDataType : IBusDataType
    {
        (bool equal, bool ALarger, TBusDataType output) AreEqual(TBusDataType a, TBusDataType b, bool aboveBitIsEqual, bool aboveBitALarger);
    }
}