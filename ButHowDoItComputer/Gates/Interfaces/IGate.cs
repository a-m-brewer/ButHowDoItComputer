using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;


namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IGate
    {
        bool Apply(params bool[] bits);
    }
}