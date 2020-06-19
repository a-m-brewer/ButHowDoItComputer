using System.Collections.Generic;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IAluWire<TBusDataType> where TBusDataType : IList<bool>
    {
        TBusDataType Apply(params TBusDataType[] input);
    }
}