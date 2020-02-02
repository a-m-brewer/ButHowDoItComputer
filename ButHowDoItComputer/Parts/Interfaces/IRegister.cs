using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IRegister<T> : ICpuEnableSubscriber, ICpuSettableSubscriber, ICpuInput<T>, IBusInputSubscriber<T>
    {
        T Data { get; set; }
        T Input { get; set; }
        T Output { get; }
        
        List<IBusInputSubscriber<T>> Subscribers { get; } 

        public string Name { get; set; }
        
        T ApplyOnce(T input, bool enable = false);
        T Apply(T input);
    }
}