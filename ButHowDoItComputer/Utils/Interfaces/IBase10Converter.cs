using System.Collections.Generic;

namespace ButHowDoItComputer.Utils.Interfaces
{
    public interface IBase10Converter
    {
        IEnumerable<bool> ToBit(uint input);
        uint ToInt(IList<bool> bits);
        IEnumerable<bool> Pad(List<bool> bits, int amount);
    }
}