using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;


namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IGate
    {
        IBit Apply(params IBit[] bits);
    }
}