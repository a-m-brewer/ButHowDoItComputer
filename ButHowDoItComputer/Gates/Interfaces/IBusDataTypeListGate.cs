using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBusDataTypeListGate<TBusDataType> where TBusDataType : IBusDataType
    {
        TBusDataType Apply(params TBusDataType[] input);
    }
}