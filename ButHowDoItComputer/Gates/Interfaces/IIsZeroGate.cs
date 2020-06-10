using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IIsZeroGate<in TBusDataType> where TBusDataType : IBusDataType
    {
        bool IsZero(TBusDataType input);
    }
}