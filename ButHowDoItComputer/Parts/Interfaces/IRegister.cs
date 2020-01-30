using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IRegister<T> : ICpuEnableSubscriber, ICpuSettableSubscriber, ICpuInput<T>
    {
        T Data { get; }
        T Input { get; set; }
        T Output { get; }
        
        T ApplyOnce(T input, bool enable = false);
        T Apply(T input);
    }
}