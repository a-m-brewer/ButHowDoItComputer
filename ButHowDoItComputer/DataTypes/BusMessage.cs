using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.DataTypes
{
    public class BusMessage<T> : INamed
    {
        public T Data { get; set; }
        public string Name { get; set; }
    }
}