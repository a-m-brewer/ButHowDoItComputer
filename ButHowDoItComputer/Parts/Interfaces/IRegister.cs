using System.Collections.Generic;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IPinPart : IEnablable, ISettable {}
    
    public interface IRegister<T> : IApplicable, IPinPart, INamed
    {
        T Data { get; set; }
        T Input { get; set; }
        T Output { get; }

        T ApplyOnce(T input, bool enable = false);
        T Apply(T input);
    }
}