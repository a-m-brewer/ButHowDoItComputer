using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IBusDataTypeRegisterFactory<TBusDataType> : IRegisterFactory<TBusDataType> where TBusDataType : IBusDataType
    {
    }
}