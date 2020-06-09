using ButHowDoItComputer.Components;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts;

namespace ButHowDoItComputer.DataTypes
{
    public class IoPinStates<TBusDataType> where TBusDataType : IBusDataType
    {
        public IoPinStates(IBusDataTypeFactory<TBusDataType> busDataTypeFactory)
        {
            Bus = new IoBus<TBusDataType>(Clk, new BusMessage<TBusDataType> {Data = busDataTypeFactory.Create(), Name = "IoBus"});
        }
        
        public SetEnable Clk { get; set; } = new SetEnable();
        public bool DataAddress { get; set; }
        public bool InputOutput { get; set; }
        public IBus<TBusDataType> Bus { get; set; }
    }
}