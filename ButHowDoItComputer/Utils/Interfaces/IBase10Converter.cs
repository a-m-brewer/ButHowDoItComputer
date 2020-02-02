using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Utils.Interfaces
{
    public interface IBase10Converter
    {
        IEnumerable<IBit> ToBit(uint input);
        uint ToInt(IList<IBit> bits);
        IEnumerable<IBit> Pad(List<IBit> bits, int amount);
    }
}