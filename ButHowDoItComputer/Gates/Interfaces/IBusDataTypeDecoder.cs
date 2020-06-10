using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeDecoder<out TBusDataType> where TBusDataType : IBusDataType
    {
        TBusDataType Decode(bool a, bool b, bool c);
    }
}