using System.Collections.Generic;

namespace ButHowDoItComputer.Utils.Interfaces
{
    public interface IBase10Converter
    {
        List<bool> ToBit(uint input);
        bool[] ToBit(uint input, int arraySize);
        uint ToInt(IList<bool> bits);
        IEnumerable<bool> Pad(List<bool> bits, int amount);
    }
}