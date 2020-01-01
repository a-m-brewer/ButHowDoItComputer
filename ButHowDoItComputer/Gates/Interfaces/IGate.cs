using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;


namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IGate
    {
        IBit Apply(IEnumerable<IBit> bits);
    }
}