using System.Collections.Generic;

namespace ButHowDoItComputer.DataTypes.Interfaces
{
    public interface IBusDataTypeFactory<out TBusDataType> where TBusDataType : IList<bool>
    {
        TBusDataType Create();
        TBusDataType CreateParams(bool[] bits);
        TBusDataType Create(IList<bool> bits);
        TBusDataType Create(uint input);
    }
}