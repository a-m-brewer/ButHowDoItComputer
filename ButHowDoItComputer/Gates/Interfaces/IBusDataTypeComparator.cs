using System.Collections.Generic;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeComparator<TBusDataType> where TBusDataType : IList<bool>
    {
        (bool equal, bool ALarger, TBusDataType output) AreEqual(TBusDataType a, TBusDataType b, bool aboveBitIsEqual, bool aboveBitALarger);
    }
}