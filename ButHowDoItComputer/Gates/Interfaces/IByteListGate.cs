using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IByteListGate
    {
        IByte Apply(IList<IByte> input);
    }
}