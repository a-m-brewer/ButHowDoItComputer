using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeDecoder<out TBusDataType> where TBusDataType : IList<bool>
    {
        TBusDataType Decode(bool a, bool b, bool c);
    }
}