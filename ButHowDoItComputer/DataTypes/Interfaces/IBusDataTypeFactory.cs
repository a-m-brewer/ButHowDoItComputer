namespace ButHowDoItComputer.DataTypes.Interfaces
{
    public interface IBusDataTypeFactory<out TBusDataType> where TBusDataType : IBusDataType
    {
        TBusDataType Create();
        TBusDataType Create(bool[] bits);
    }
}