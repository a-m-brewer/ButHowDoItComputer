using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Codes.ASCII.Interfaces
{
    public interface IByteToAsciiConverter
    {
        IByte ToByte(string input);
        string ToAscii(IByte input);
    }
}