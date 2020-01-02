using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Codes.ASCII.Interfaces
{
    public interface IByteToBase10Converter
    {
        int ToInt(IByte input);
        IByte ToByte(int input);
    }
}