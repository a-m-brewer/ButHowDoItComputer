using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IInverter<TBusDataType> where TBusDataType : IList<bool>
    {
        TBusDataType Invert(TBusDataType input);
    }
}