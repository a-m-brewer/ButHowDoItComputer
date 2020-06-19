using System.Collections.Generic;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IInverter<TBusDataType> where TBusDataType : IList<bool>
    {
        TBusDataType Invert(TBusDataType input);
    }
}