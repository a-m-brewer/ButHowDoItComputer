using System.Collections.Generic;

namespace ButHowDoItComputer.Codes.ASCII.Interfaces
{
    public interface IByteToAsciiConverter
    {
        IList<bool> ToByte(string input);
        string ToAscii(IList<bool> input);
    }
}