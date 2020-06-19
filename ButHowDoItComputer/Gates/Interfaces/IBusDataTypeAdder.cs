using System.Collections.Generic;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeAdder<TBusDataType> where TBusDataType : IList<bool>
    {
        (TBusDataType Sum, bool CarryOut) Add(TBusDataType a, TBusDataType b, bool carryIn);
    }
}