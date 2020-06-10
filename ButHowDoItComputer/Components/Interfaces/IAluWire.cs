using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IAluWire<TBusDataType> where TBusDataType : IBusDataType
    {
        TBusDataType Apply(params TBusDataType[] input);
    }
}