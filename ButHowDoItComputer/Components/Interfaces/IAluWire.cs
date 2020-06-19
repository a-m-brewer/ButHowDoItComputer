using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IAluWire<TBusDataType> where TBusDataType : IList<bool>
    {
        TBusDataType Apply(params TBusDataType[] input);
    }
}