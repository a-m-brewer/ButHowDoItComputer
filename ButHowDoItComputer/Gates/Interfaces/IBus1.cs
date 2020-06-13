using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IBus1<TBusDataType> : IBusDataTypeGate<TBusDataType>, ISettable, IApplicable, IOutputable<TBusDataType>, IInputable<TBusDataType> where TBusDataType : IBusDataType
    {
    }
}