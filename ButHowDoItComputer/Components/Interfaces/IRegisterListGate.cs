using System.Collections.Generic;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IRegisterListGate
    {
        void Apply(IList<IRegister> inputRegisters, IRegister outputRegister);
    }
}