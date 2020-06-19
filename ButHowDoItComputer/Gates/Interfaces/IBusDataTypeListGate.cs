using System.Collections.Generic;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeListGate<TBusDataType> where TBusDataType : IList<bool>
    {
        TBusDataType Apply(params TBusDataType[] input);
    }
}