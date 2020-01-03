using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Components
{
    public interface IBus : IList<IRegister>
    {
        void Apply();
        IByte State { get; }
    }
}