using System.Collections.Generic;

namespace ButHowDoItComputer.Utils.Interfaces
{
    public interface IByteToBase10Converter
    {
        uint ToInt(IList<bool> input);
        IList<bool> ToByte(uint input);
    }
}