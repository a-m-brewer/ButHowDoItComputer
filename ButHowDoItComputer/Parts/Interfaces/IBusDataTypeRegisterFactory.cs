using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IBusDataTypeRegisterFactory<TBusDataType> : IRegisterFactory<TBusDataType> where TBusDataType : IBusDataType
    {
    }
}