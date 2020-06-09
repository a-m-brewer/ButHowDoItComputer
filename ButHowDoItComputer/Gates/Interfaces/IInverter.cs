using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IInverter<TBusDataType> where TBusDataType : IBusDataType
    {
        TBusDataType Invert(TBusDataType input);
    }
}