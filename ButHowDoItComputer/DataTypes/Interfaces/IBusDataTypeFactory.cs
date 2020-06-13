using System.Collections.Generic;

namespace ButHowDoItComputer.DataTypes.Interfaces
{
    public interface IBusDataTypeFactory<out TBusDataType> where TBusDataType : IBusDataType
    {
        TBusDataType Create();
        TBusDataType CreateParams(bool[] bits);
        TBusDataType Create(IList<bool> bits);
        TBusDataType Create(uint input);
    }
}