using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IRegisterListGate
    {
        void Apply(IList<IRegister<IByte>> inputRegisters, IRegister<IByte> outputRegister);
    }
}