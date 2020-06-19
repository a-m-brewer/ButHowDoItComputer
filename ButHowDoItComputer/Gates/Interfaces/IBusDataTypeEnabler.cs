using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeEnabler<TBusDataType> : IBusDataTypeGate<TBusDataType> where TBusDataType : IList<bool>
    {
    }
}