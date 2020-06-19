using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IIsZeroGate<in TBusDataType> where TBusDataType : IList<bool>
    {
        bool IsZero(TBusDataType input);
    }
}