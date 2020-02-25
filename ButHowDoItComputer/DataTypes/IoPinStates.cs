using ButHowDoItComputer.Components;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts;

namespace ButHowDoItComputer.DataTypes
{
    public class IoPinStates
    {
        public IoPinStates()
        {
            Bus = new IoBus<IByte>(Clk, new BusMessage<IByte> {Data = new Byte(), Name = "IoBus"});
        }
        
        public SetEnable Clk { get; set; } = new SetEnable();
        public bool DataAddress { get; set; }
        public bool InputOutput { get; set; }
        public IBus<IByte> Bus { get; set; }
    }
}