using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IBus : IList<IRegister<IByte>>
    {
        void Apply();
        IByte State { get; }
    }
}